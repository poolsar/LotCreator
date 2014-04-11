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
		old.CopyInfo = 'unact';
		