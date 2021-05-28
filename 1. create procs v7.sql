
--PROCEDURES and views

alter Procedure v_ProjectAdd (@projectName varchar(50), @projectDescription varchar(200)='',@startDate datetime='')
AS
if @startDate=''
set @startDate= getdate()
--exec v_ProjectAdd 'test'
INSERT INTO [dbo].[v_Project]([ProjectName],[ProjectDesc],[StartDate])
     VALUES (@projectName, @projectDescription,   @startDate)
GO


alter procedure v_RunAdd (@project_k int, @runName nchar(500), @RunDate datetime = null )
--exec v_RunAdd 1, 'Test1' , '20200101'
AS
if @RunDate is null
select @RunDate = getdate()


INSERT INTO [dbo].[v_Run]
           ([Project_K],[RunName],[RunDate])
     VALUES
           (@project_k,@runName,@RunDate)
GO


alter procedure v_RuleAdd(@Project_K int,@RuleName varchar(50),@RuleDesc varchar(1000),@RuleType_ID int,@Section varchar(20),@RuleOrder int,
					@RuleHeaderSQL varchar(500) = NULL,@RuleBodySQL varchar(1000) = NULL, @ValidationType_ID int = NULL,
	@Option1 varchar(50) =NULL ,
	@Option2 [varchar](50) =NULL,
	@Option3 [varchar](50) =NULL,
@Option4 [varchar](50) =NULL,
	@Option5 [varchar](50) =NULL,
	@Option6 varchar(50) =NULL ,
	@Option7 [varchar](50) =NULL,
	@Option8 [varchar](50) =NULL,
	@Option9 [varchar](50) =NULL,
	@Option10 [varchar](50) =NULL
	)
AS
INSERT INTO [dbo].[v_Rules] ([Project_K],[RuleName] ,[RuleDesc],[RuleType_ID],[Section],[RuleOrder],[RuleHeaderSQL],[RuleBodySQL],[ValidationType_ID],
			[Option1],[Option2],[Option3],[Option4],[Option5],[Option6],[Option7],[Option8],[Option9],[Option10]
			)
     VALUES
           (@Project_K,  @RuleName,@RuleDesc,@RuleType_ID,@Section,@RuleOrder,@RuleHeaderSQL,@RuleBodySQL,@ValidationType_ID,
			@Option1,@Option2,@Option3,@Option4,@Option5,@Option6,@Option7,@Option8,@Option9,@Option10)


GO


alter VIEW vv_run
AS
select r.Run_K,p.ProjectName,r.RunName,r.RunDate
from v_run r
  left outer join v_project p on p.project_k=r.project_k

ALTER VIEW vv_rules
AS
--bhavesh add ruletype to mapping 1=filestop, 2=error, 3=warning
select Rule_K, p.ProjectName,ru.Section,ru.RuleName,ru.RuleDesc,ru.RuleType_ID,ru.RuleHeaderSQL,ru.RuleBodySQL
from v_rules ru
  left outer join v_project p on p.project_k=ru.project_k
GO

ALTER View vv_ErrorDetails
AS
select Error_K,e.Run_k,p.ProjectName,r.runname, r.RunDate, ru.Section,ru.RuleName, ru.RuleDesc, ru.RuleType_ID,	 et.mt_to ErrorType, Source_link,	SourceValue,SourceValue2,	SourceRowNum
from v_error e
  left OUTER JOIN v_run r on r.Run_K=e.run_k
  LEFT OUTER JOIN v_project p on p.project_k=r.Project_K
  LEFT OUTER JOIN v_rules ru ON ru.rule_k=e.Rule_k
  left outer join map_item et on et.mt_k=50 and et.mt_from=ru.RuleType_ID


  ALTER VIEW vv_Error_Summary
AS
select r.Run_k,p.ProjectName,r.runname, r.RunDate, ru.Section,ru.RuleName, ru.RuleDesc, ru.RuleType_ID, et.mt_to ErrorType,  ct errors, Sample_Source_Link1, Sample_Source_Link2,Sample_SourceRowNum1, Sample_SourceRowNum2,RuleOrder, ru.Rule_K
from v_project p
   left OUTER JOIN v_run r on r.project_k=p.project_k
	LEFT OUTER JOIN v_rules ru ON ru.project_k=p.project_k
	LEFT OUTER JOIN (select Run_k, Rule_k,count(*) ct,min(source_link) Sample_Source_Link1, max(source_link) Sample_Source_Link2,min(SourceRowNum) Sample_SourceRowNum1,max(SourceRowNum) Sample_SourceRowNum2
	 FROM v_error e
	 GROUP BY Run_k,Rule_k
	 )e on e.rule_k=ru.rule_k and e.run_k=r.run_k
	left outer join map_item et on et.mt_k=50 and et.mt_from=ru.RuleType_ID


------------------


/*
INSERT INTO [dbo].[v_Error] ([Run_k],[Rule_k],[Source_link],[SourceValue],[SourceValue2],[SourceRowNum])  82, 1, 
SELECT   convert(varchar(4),o.facility)+'-'+convert(varchar(6),o.EmployeeNum) [Source_k],o.employeenum [SourceValue],n.employeenum [SourceValue2],o.rn [SourceRowNum] from  htest.dbo.[i_SOSPeopleExport] o
	left outer join htest.dbo.[i_SOSPeopleExportADP] n on convert(varchar(4),n.facility)+'-'+convert(varchar(6),n.employeenum) =convert(varchar(4),o.facility)+'-'+convert(varchar(6),o.EmployeeNum)
  where n.employeenum is null
  */
---------------

/*
--Validation Entry rules
SELECT        m_k TheKey,  mt_to Validation_Type, option1, option2, option3, option4, option5,option6, option7, option8, option9, option10
FROM            map_item
WHERE        (mt_k = 53)

*/


alter  procedure v_RunDo (@project_k int, @runName nchar(500)='No Name Sent')
AS
--sample: exec v_RunDo 1, 'Test- Census match and termination match'

DECLARE @thesql varchar(5000)
--CREATE THE RUNRECORD and get id
DECLARE @Run_K int
exec v_RunAdd @project_k, @runName
select @Run_K=@@identity--scope_identity() 

DECLARE @Rule_k int,
		@RuleHeaderSQL varchar(500),
        @RuleBodySQL  varchar(5000),
		@ValidationType_ID int=0,
		 @option1 varchar(50)='', 
		 @option2 varchar(50)='',  
		 @option3 varchar(50)='',
		 @option4 varchar(50)='',
		 @option5 varchar(50)='',
		 @option6 varchar(50)='',
		 @option7 varchar(50)='',
		 @option8 varchar(50)='',
		 @option9 varchar(50)='',
		 @option10 varchar(50)=''

		Declare @rnfield varchar(50),
				@linkmatchfield varchar(50)


DECLARE db_cursor CURSOR FOR 
select Rule_k,RuleHeaderSQL,RuleBodySQL,ValidationType_ID,option1,option2,option3,option4,option5,option6,option7,option8,option9,option10
from v_rules ru
where ru.project_k=@project_k



OPEN db_cursor  
FETCH NEXT FROM db_cursor INTO @Rule_k,@RuleHeaderSQL,@RuleBodySQL,@ValidationType_ID,@option1,@option2,@option3,@option4,@option5,@option6,@option7,@option8,@option9,@option10

WHILE @@FETCH_STATUS = 0  
BEGIN  
	--print 'run_k'
	--print @Run_K
	print 'rule_k'
	print @Rule_k
	--  print  @RuleHeaderSQL
	--  print @RuleBodySQL
	--  print 'here'

			--set the rnfield based on option4. note not all items use @option4=@rn
	if @option4 = 'Yes'
		set @rnfield= ' s.rn '
	else if @option4 >''
		set @rnfield= @option4--'s.rn'
	else set @rnfield=' NULL ';
	print 'rnfield'
	print @rnfield


	--type of match link can be value=mt_to, or key=m_k or code = mt_from
	if @option6 >''
		set @linkmatchfield= @option6--'s.rn'
	else if isnull(@option6,'') = ''
		set @linkmatchfield= ' Mt_to '
	

	set @thesql='INSERT INTO [dbo].[v_Error] ([Source_link],[SourceValue],[SourceValue2],[SourceRowNum],[Run_k],[Rule_k]) '
	 -------------
	--   select @ValidationType_ID=1912 --'required'
	--select @option2='LastName'
	--select @option1= 'htest.dbo.[ds_Person]'




	IF @ValidationType_ID=1920 or @ValidationType_ID is null or @ValidationType_ID=0 or @ValidationType_ID='' --'Custom' OR Blank
	  print 'Validation Type: SQL'
	ELSE If @ValidationType_ID= 1912 --'required'--option1 is link, option2 is table, option3 is field, option4 is row number- SQL code fields not used
	BEGIN
		print 'Validation Type: Required'
		select @RuleHeaderSQL='SELECT '+@option1+' [Source_k],'+@option3+' [SourceValue],null [SourceValue2],'+@rnfield+' [SourceRowNum] '
		select @RuleBodySQL='from '+@option2+' s
			where '+'isnull('+@option3+','''')='''' '
	END
	ELSE If @ValidationType_ID= 1916 --'Field length'--option1 is link, option2 is table, option3 is field, option4 is row number, option5 is length- SQL code fields not used
	BEGIN
		print 'Validation Type: Field length'
		select @RuleHeaderSQL='SELECT '+@option1+' [Source_k],'+@option3+' [SourceValue],null [SourceValue2],'+@rnfield+' [SourceRowNum] '
		select @RuleBodySQL='from '+@option2+' s
			where NOT('+'isnull('+@option3+','''')='''') AND '+'len('+@option3+') <>'+@option5+' '

	END
	ELSE If @ValidationType_ID= 1918 --'SSN Format'--option1 is link, option2 is table, option3 is field, option4 is row number, option5 is length- SQL code fields not used
	BEGIN
		print 'Validation Type: SSN Format'
		select @RuleHeaderSQL='SELECT '+@option1+' [Source_k],'+@option3+' [SourceValue],null [SourceValue2],'+@rnfield+' [SourceRowNum] '
		select @RuleBodySQL='from '+@option2+' s
			where [dbo].[v_ValidSSN]('+@option3+')<>1 '

	END
	ELSE If @ValidationType_ID= 1919 --'Email Format'--option1 is link, option2 is table, option3 is field, option4 is row number, option5 is length- SQL code fields not used
	BEGIN
		print 'Validation Type: Email Format'
		select @RuleHeaderSQL='SELECT '+@option1+' [Source_k],'+@option3+' [SourceValue],null [SourceValue2],'+@rnfield+' [SourceRowNum] '
		select @RuleBodySQL='from '+@option2+' s
			where [dbo].[v_ValidEmail]('+@option3+')<>1 '

	END
	ELSE If @ValidationType_ID= 1914 --'Valid List'--option1 is link, option2 is table, option3 is field, option4 is row number, option5 is length- SQL code fields not used
	BEGIN
		print 'Validation Type: Valid List'
		select @RuleHeaderSQL='SELECT '+@option1+' [Source_k],'+@option3+' [SourceValue],null [SourceValue2],'+@rnfield+' [SourceRowNum] '
		select @RuleBodySQL='from '+@option2+' s
							where not('+@option3+' in (select '+@option6+' from c_config.dbo.map_item where mt_k='+@option5+')) '
	END
	ELSE If @ValidationType_ID= 1921 --'Out of Range Date'--option1 is link, option2 is table, option3 is field, option4 is row number, option5 is length- SQL code fields not used
	BEGIN
		print 'Validation Type: Out of Range Date'
		select @RuleHeaderSQL='SELECT '+@option1+' [Source_k],'+@option3+' [SourceValue],null [SourceValue2],'+@rnfield+' [SourceRowNum] '
		select @RuleBodySQL='from '+@option2+' s
			where '+@option3+' >= getdate() and '+@option3+' <= ''19000101'' '

	END
	/*
	Select p.Person_K, count(ec.EmergencyContact_K) 
from dw_Person p
	left outer join dw_EmergencyContact ec on ec.Person_K=p.Person_K
group by p.Person_K
having count(ec.Person_K)=0
*/
	ELSE
		print 'ERROR- pick a validation type'



 select @thesql=@thesql+@RuleHeaderSQL+', '+convert(varchar(10),@Run_K)+', '+convert(varchar(10),@Rule_k) +' '+ @RuleBodySQL


 	  print @thesql
	  exec (@thesql)
      FETCH NEXT FROM db_cursor INTO @Rule_k,@RuleHeaderSQL,@RuleBodySQL,@ValidationType_ID,@option1,@option2,@option3,@option4,@option5,@option6,@option7,@option8,@option9,@option10

END 

CLOSE db_cursor  
DEALLOCATE db_cursor 
GO




ALTER FUNCTION dbo.v_ValidSSN
(
    @value VARCHAR(11)

	--select dbo.v_ValidSSN('123456789')
)
RETURNS BIT
AS
BEGIN
    DECLARE @result BIT;
    IF (@value LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
        SET @result = 1;
    ELSE
        SET @result = 0;

    RETURN @result;
END;

GO

	CREATE Function v_Validemail (@email_address varchar(100))
	returns bit
	AS
	--	select dbo.validate_email('testvestracare.com')
	BEGIN
			IF (
			 CHARINDEX(' ',LTRIM(RTRIM(@email_address))) = 0 
		AND  LEFT(LTRIM(@email_address),1) <> '@' 
		AND  RIGHT(RTRIM(@email_address),1) <> '.' 
		AND  CHARINDEX('.',@email_address ,CHARINDEX('@',@email_address)) - CHARINDEX('@',@email_address ) > 1 
		AND  LEN(LTRIM(RTRIM(@email_address ))) - LEN(REPLACE(LTRIM(RTRIM(@email_address)),'@','')) = 1 
		AND  CHARINDEX('.',REVERSE(LTRIM(RTRIM(@email_address)))) >= 3 
		AND  (CHARINDEX('.@',@email_address ) = 0 AND CHARINDEX('..',@email_address ) = 0)
		)
		   return 1
		ELSE
		   return 0
		return 0
	END

--list of errors
select mt_to Error_Level, option1 Error_Description
from c_config.dbo.map_item
where mt_k=50

