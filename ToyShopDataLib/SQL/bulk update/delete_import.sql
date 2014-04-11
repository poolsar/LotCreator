DELETE FROM [dbo].[BegemotProductSet]
      WHERE  Id in 
	  (
			SELECT new.[Id]
			  
			FROM [BegemotProductSet] new
				left join [BegemotProductSet] old on 
				old.[Article] = new.[Article] 

			where new.CopyInfo = 'import' AND 
				not (old.CopyInfo = 'import')	  
	  )
GO


