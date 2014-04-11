-- обновляем измененные

MERGE INTO [BegemotProductSet] old
   USING [BegemotProductSet] new 
      ON old.[Article] = new.[Article]
         AND old.CopyInfo is null
		 and new.CopyInfo = 'import' 
		 and (old.[Count] <> new.[Count]
		 or old.[RetailPrice] <> new.[RetailPrice]
		 or old.[WholeSalePrice]<> new.[WholeSalePrice]
		 or old.[Active] = 0)

WHEN MATCHED THEN
   UPDATE 
      SET
		old.CopyInfo = 'mod',
		old.[Count] = new.[Count],
		old.[RetailPrice] = new.[RetailPrice],
		old.[WholeSalePrice]= new.[WholeSalePrice],
		old.[DateUpdate] = new.[DateUpdate],
		old.[Active] = 1;
--GO

-- деактивируем отсутствующие в новом прайсе

declare @dateupdate datetime
set @dateupdate = (select max([DateUpdate]) from [BegemotProductSet])

MERGE INTO [BegemotProductSet] old
   USING (
   			SELECT old.[Id]
			FROM [BegemotProductSet] old
   			left join [BegemotProductSet] new on 
				old.[Article] = new.[Article] AND 
				old.CopyInfo is null and 
				new.CopyInfo = 'import'

			where old.[CopyInfo] is null and new.Id is null
		 ) new 
      
	  ON old.Id = new.Id

WHEN MATCHED THEN
   UPDATE 
      SET 
		old.[Count] = 0,
		old.[Active] = 0,
		old.[DateUpdate] = @dateupdate,
		old.CopyInfo = 'unact';
--GO

-- удаляем импортированные старые
 
DELETE FROM [dbo].[BegemotProductSet]
      WHERE  Id in 
	  (
			SELECT new.[Id]
			  
			FROM [BegemotProductSet] new
				left join [BegemotProductSet] old on 
				old.[Article] = new.[Article] 

			where new.CopyInfo = 'import'  
				 AND (old.CopyInfo <> 'import' or old.CopyInfo is null)	  
	  );
--GO

-- отчищаем техническую информацию

UPDATE [dbo].[BegemotProductSet]
   SET [CopyInfo] = null;
--GO


---

-- обновляем историю количества товара

INSERT INTO [dbo].[BegemotCountHistorySet]
           ([Article]
           ,[Count]
           ,[DateCreate]
		   ,[BegemotProduct_Id])
    SELECT 
		p.Article,		
		p.[Count] newCount,
		p.[DateUpdate] newDate,
		p.[Id]		

	FROM [BegemotProductSet] p

	left join 
	(
		SELECT his.Article,
				Max(his.DateCreate) DateCreate
		
		FROM [BegemotCountHistorySet] his
		group by his.Article
	 
	) artDate

	on	p.Article = artDate.Article 

	left join [BegemotCountHistorySet] artCount on 
		artDate.Article = artCount.Article and  
		artDate.DateCreate = artCount.DateCreate 
	
	where 
		p.[Count] <> artCount.[Count] or
		artCount.[Count] is null;
	--GO


-- обновляем историю цен товара

INSERT INTO [dbo].[BegemotPriceHistorySet]
      ([Article]
      ,[RetailPrice]
      ,[WholeSalePrice]
      ,[DateCreate]
	  ,[BegemotProduct_Id])

SELECT 
	 p.[Article]		
	,p.[RetailPrice]
	,p.[WholeSalePrice]
	,p.[DateUpdate]
	,p.[Id]

	FROM [BegemotProductSet] p

	left join 
	(
		SELECT his.Article,
				Max(his.DateCreate) DateCreate
		
		FROM [BegemotPriceHistorySet] his
		group by his.Article
	 
	) artDate
	on	p.Article = artDate.Article 

	left join [BegemotPriceHistorySet]  artPrice on 
		artDate.Article = artPrice.Article and  
		artDate.DateCreate = artPrice.DateCreate 
	
	where 
		p.WholeSalePrice <> artPrice.WholeSalePrice or
		p.RetailPrice <> artPrice.RetailPrice or
		artPrice.RetailPrice  is null;
	--GO
