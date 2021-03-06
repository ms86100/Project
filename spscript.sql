USE [FileUploadEngine]
GO
/****** Object:  UserDefinedFunction [dbo].[v_Validemail]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE Function [dbo].[v_Validemail] (@email_address varchar(100))
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
GO
/****** Object:  UserDefinedFunction [dbo].[v_ValidSSN]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[v_ValidSSN]
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
/****** Object:  View [dbo].[vv_Error_Summary]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create VIEW [dbo].[vv_Error_Summary]
AS
select r.Run_k,p.ProjectName,r.runname, r.RunDate, ru.Section,ru.RuleName, ru.RuleDesc, ru.RuleType_ID, et.mt_to ErrorType, 
ct errors, Sample_Source_Link1, Sample_Source_Link2,Sample_SourceRowNum1, Sample_SourceRowNum2,RuleOrder, ru.Rule_K
from v_project p
   left OUTER JOIN v_run r on r.project_k=p.project_k
	LEFT OUTER JOIN v_rules ru ON ru.project_k=p.project_k
	LEFT OUTER JOIN (select Run_k, Rule_k,count(*) ct,min(source_link) Sample_Source_Link1, max(source_link) Sample_Source_Link2,min(SourceRowNum) Sample_SourceRowNum1,max(SourceRowNum) Sample_SourceRowNum2
	 FROM v_error e
	 GROUP BY Run_k,Rule_k
	 )e on e.rule_k=ru.rule_k and e.run_k=r.run_k
	left outer join map_item et on et.mt_k=50 and et.mt_from=ru.RuleType_ID

GO
/****** Object:  StoredProcedure [dbo].[c_logAdd]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[c_logAdd]
(@logh_k bigint, @resCount decimal(18, 2) = null, @CountTable varchar(500) = null, @logstep varchar(30) , @logresult varchar(10) = 'inprogress',
@lognote varchar(500) = null) --SIMPLE: exec c_logAdd 'SOS'
--one set: Exec c_logAdd @process='SOS', @logstatus='done'
--Should use: Exec c_logAdd @process='SOS', @logend=null, @logstatus='Started', @logresult='inprogress'--note cant use getdate function to send to a procedure
AS 
declare @TheSQL nvarchar(1000) = '' if @CountTable > '' 
BEGIN
   set
      @TheSQL = 'select @resCountout=count(*) FROM ' + @CountTable 		--print @TheSQL
      EXECUTE SP_EXECUTESQL @TheSQL, N'@resCountout decimal(18,2) OUTPUT', @resCountout = @resCount OUTPUT 
END
INSERT INTO
   [dbo].[c_logd] ([logh_k] , [logtime] , [resCount] , [logstep] , [logdresult] , [logNote]) 
VALUES
   (
      @logh_k , getdate() , @resCount , @logstep , @logresult , @lognote
   )

   SELECT CAST(SCOPE_IDENTITY() AS INT) AS result



GO
/****** Object:  StoredProcedure [dbo].[c_logEnd]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[c_logEnd](@logh_k bigint,   @logresult varchar(10))
AS
BEGIN
DECLARE   @error_number INT, @ErMessage NVARCHAR(2048), @ErSeverity INT, @ErState INT
BEGIN TRY  
BEGIN
UPDATE [dbo].[c_logh]
   SET [logend] = getdate()
      ,[logstatus] = 'done'
      ,[logresult] = @logresult
 WHERE logh_k=@logh_k

			 set @error_number = 0
           set @ErMessage = ''
 END

 	END TRY 
	BEGIN CATCH  
		SELECT @error_number = ERROR_NUMBER(), @ErMessage = ERROR_MESSAGE(),  @ErSeverity = ERROR_SEVERITY(), @ErState = ERROR_STATE()  
		
		IF @error_number = 2601 -- check constraint violation   
			BEGIN
				set @error_number = 2601    
				set @ErMessage = 'Error while updating log end logh_k dosent match'
			END  
		ELSE -- some other "untrapped" error has occured   
		BEGIN 
			RAISERROR (@ErMessage, @ErSeverity, @ErState )
		END 
	END CATCH
	select @error_number as ErrorCode, @ErMessage as ErrorMessage
END
GO
/****** Object:  StoredProcedure [dbo].[c_logStart]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[c_logStart]
(
@process nchar(10), 
@logrunname nchar(50)=null, 
@start datetime=null, 
@logend datetime=null,
@logstatus nchar(10)='Started'
,@logresult nchar(10)='inprogress')
--SIMPLE: exec c_logAdd 'SOS'
--one set: Exec c_logAdd @process='SOS', @logstatus='done'
--Should use: Exec c_logAdd @process='SOS', @logend=null, @logstatus='Started', @logresult='inprogress'--note cant use getdate function to send to a procedure
AS
if @start is null 	set @start=getdate()
if @logend is null and @logstatus='done'	set @logend=getdate()
if @logstatus='done' and @logresult is null set @logresult='success'

  INSERT INTO [dbo].[c_logh]
           ([logprocess]
           ,[logstart]
           ,[logend]
           ,[logstatus]
           ,[logresult]
		   ,[LogRunName])
     VALUES
           (@process
           , @start
           , @logend
           ,@logstatus
           , @logresult
		   ,@LogRunName)

		
SELECT CAST(SCOPE_IDENTITY() AS INT) AS logh_k
	     
GO
/****** Object:  StoredProcedure [dbo].[sp_ExecPackage]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- =============================================  
CREATE PROCEDURE [dbo].[sp_ExecPackage] (
  --@EmpCode Varchar(10), 
  @PackagePath VARCHAR(2000)
--@EmpName varchar (10)
)
AS
    DECLARE @SQLQuery AS VARCHAR(2000)

    SET @SQLQuery = 'DTExec /FILE "' + @PackagePath + '" '

    --SET @SQLQuery = @SQLQuery + ' /SET \Package.Variables[EmpCode].Value;'+ @EmpCode
    --SET @SQLQuery = @SQLQuery + ' /SET \Package.Variables[EmpName].Value;'+ @EmpName
    EXEC master..Xp_cmdshell
      @SQLQuery

    IF @@ERROR <> 0
      GOTO errorhandler

    SET nocount OFF

    RETURN( 0 )

    ERRORHANDLER:

    RETURN( @@ERROR ) 
GO
/****** Object:  StoredProcedure [dbo].[udspGetDataFromStagingToProduction]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- =============================================  
CREATE PROCEDURE [dbo].[udspGetDataFromStagingToProduction]
@FromTable VARCHAR(max)=NULL,
@ToTable   VARCHAR(max)=NULL
AS
  BEGIN
      DECLARE @SQL VARCHAR(max)

      SET @SQL= 'INSERT  INTO ' + @ToTable + ' SELECT  * FROM '
                + @FromTable + ''
      EXEC(@SQL)
  END 
GO
/****** Object:  StoredProcedure [dbo].[udspGetJobDetails]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- =============================================  
CREATE PROC [dbo].[udspGetJobDetails]
  @ImportID INT null
AS
  BEGIN
    IF(@ImportID <> '')
    BEGIN
      SELECT     i.import_k,
                 s.steporder,
                 i.importname,
                 s.action,
                 s.parameter1,
                 s.parameter2,
                 s.parameter3,
                 s.parameter4,
                 s.parameter5,
                 s.parameter6,
                 s.parameter7,
                 i.isactive,
                 i.effectivefrom,
                 i.effectiveto
      FROM       c_importjob i
      INNER JOIN c_importsteps s
      ON         i.import_k=s.import_k
      WHERE      i.import_k=@ImportID
      ORDER BY   import_k,
                 steporder
    END
    ELSE
    BEGIN
      SELECT     i.import_k,
                 s.steporder,
                 i.importname,
                 s.action,
                 s.parameter1,
                 s.parameter2,
                 s.parameter3,
                 s.parameter4,
                 s.parameter5,
                 s.parameter6,
                 s.parameter7,
                 i.isactive,
                 i.effectivefrom,
                 i.effectiveto
      FROM       c_importjob i
      INNER JOIN c_importsteps s
      ON         i.import_k=s.import_k
      WHERE      isactive=1
      AND        getdate() BETWEEN effectivefrom AND        effectiveto
      ORDER BY   import_k,
                 steporder
    END
  END
GO
/****** Object:  StoredProcedure [dbo].[udspGetmappingdata]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- =============================================  
CREATE PROC [dbo].[udspGetmappingdata]
@ImportMapHeader_k INT
AS
  BEGIN
      SELECT Map_k,
             FromField,
             ToField
      FROM   c_importmapheader MH
             INNER JOIN c_importmap TM
                     ON MH.importmapheader_k = TM.importmapheader_k
      WHERE  TM.importmapheader_k = @ImportMapHeader_k
  END 
GO
/****** Object:  StoredProcedure [dbo].[udspGetValidationData]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- =============================================  
CREATE PROC [dbo].[udspGetValidationData] @project_k INT
AS
  BEGIN
      EXEC V_rundo
        @project_k,
        'FIE run'
  END 
GO
/****** Object:  StoredProcedure [dbo].[udspGetValidationResult]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- =============================================  
CREATE PROC [dbo].[udspGetValidationResult]
  @project_k INT
AS
  BEGIN
  DECLARE @error_number INT,
      @ErMessage          NVARCHAR(2048),
      @ErSeverity         INT,
      @ErState            INT
	  DECLARE @Result       INT
	  BEGIN try
      SET @Result=
      (
             SELECT Sum(Isnull(errors,0)) Result
             FROM   vv_error_summary
             WHERE  run_k=
                    (
                           SELECT Max(run_k)
                           FROM   v_run
                           WHERE  project_k=@project_k)
             AND    errortype='Severe'
             AND    Isnull(errors,0)>0)
      IF(@Result IS NOT NULL)
      BEGIN
        SELECT 1       AS ErrorCode,
               'Error' AS ErrorMessage
      END
      ELSE
      BEGIN
        SELECT 0  AS ErrorCode,
               '' AS ErrorMessage
      END
    END try
	BEGIN catch
      SELECT @error_number = Error_number(),
             @ErMessage = Error_message(),
             @ErSeverity = Error_severity(),
             @ErState = Error_state() RAISERROR (@ErMessage, @ErSeverity, @ErState )
    END catch
	SELECT @error_number AS ErrorCode,
           @ErMessage    AS ErrorMessage
  end
GO
/****** Object:  StoredProcedure [dbo].[udspMappingInsert]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- =============================================  
CREATE PROCEDURE [dbo].[udspMappingInsert] @TableName VARCHAR(50)  
AS  
  BEGIN  
      DECLARE @error_number INT,  
              @ErMessage    NVARCHAR(2048),  
              @ErSeverity   INT,  
              @ErState      INT  
  
      BEGIN try  
          INSERT INTO c_importmapheader  
          VALUES     (@TableName)  
      END try  
  
      BEGIN catch  
          SELECT @error_number = Error_number(),  
                 @ErMessage = Error_message(),  
                 @ErSeverity = Error_severity(),  
                 @ErState = Error_state()  
  
          IF @error_number = 2601 -- check constraint violation       
            BEGIN  
                SET @error_number = 2601  
                SET @ErMessage = @TableName  
                                 + ' already exists, please check the record...'  
            END  
          ELSE -- some other "untrapped" error has occured       
            BEGIN  
                RAISERROR (@ErMessage,@ErSeverity,@ErState )  
            END  
      END catch  
  
      SELECT @error_number AS ErrorCode,  
             @ErMessage    AS ErrorMessage  
  END 
GO
/****** Object:  StoredProcedure [dbo].[udspMstCheckIfTableExist]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- ============================================= 
CREATE PROC [dbo].[udspMstCheckIfTableExist] @TN VARCHAR(max)
AS
  BEGIN
      IF Object_id(@TN, 'U') IS NOT NULL
        BEGIN
            SELECT ( 'Found' ) AS Result
        END
      ELSE
        BEGIN
            SELECT ( 'NotFound' ) AS Result
        END
  END 
GO
/****** Object:  StoredProcedure [dbo].[udspMstColumnRename]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- =============================================  
CREATE PROC [dbo].[udspMstColumnRename]
@TableAndColumnName VARCHAR(max),
@NewColumnName      VARCHAR(max)
AS
  BEGIN
      EXEC Sp_rename
        @TableAndColumnName,
        @NewColumnName,
        'COLUMN'
  END 
GO
/****** Object:  StoredProcedure [dbo].[udspMstMapHeaderTableInsert]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Sagar Sharma  
-- =============================================  
CREATE PROCEDURE [dbo].[udspMstMapHeaderTableInsert] @TableName VARCHAR(max)
AS
  BEGIN
      DECLARE @error_number INT,
              @ErMessage    NVARCHAR(2048),
              @ErSeverity   INT,
              @ErState      INT
      DECLARE @ID INT

      BEGIN try
          SET @ID=(SELECT importmapheader_k
                   FROM   c_importmapheader
                   WHERE  importmapname = @TableName)

          IF( @ID <> '' )
            BEGIN
                SELECT importmapheader_k
                FROM   c_importmapheader
                WHERE  importmapname = @TableName
            END
          ELSE
            BEGIN
                INSERT INTO c_importmapheader
                VALUES     (@TableName)

                SELECT Scope_identity()
            END
      END try

      BEGIN catch
          SELECT @error_number = Error_number(),
                 @ErMessage = Error_message(),
                 @ErSeverity = Error_severity(),
                 @ErState = Error_state()

          IF @error_number = 2601 -- check constraint violation     
            BEGIN
                SET @error_number = 2601
                SET @ErMessage = @TableName
                                 + ' already exists, please check the record...'
            END
          ELSE -- some other "untrapped" error has occured     
            BEGIN
                RAISERROR (@ErMessage,@ErSeverity,@ErState )
            END
      END catch

      SELECT @error_number AS ErrorCode,
             @ErMessage    AS ErrorMessage
  END 
GO
/****** Object:  StoredProcedure [dbo].[v_ProjectAdd]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[v_ProjectAdd] (@projectName varchar(50), @projectDescription varchar(200)='',@startDate datetime='')
AS
if @startDate=''
set @startDate= getdate()
--exec v_ProjectAdd 'test'
INSERT INTO [dbo].[v_Project]([ProjectName],[ProjectDesc],[StartDate])
     VALUES (@projectName, @projectDescription,   @startDate)
GO
/****** Object:  StoredProcedure [dbo].[v_RuleAdd]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[v_RuleAdd]
(@Project_K int,
@RuleName varchar(50),
@RuleDesc varchar(1000),
@RuleType_ID int,
@Section varchar(20),
@RuleOrder int,
@RuleHeaderSQL varchar(500) = NULL,
@RuleBodySQL varchar(1000) = NULL, 
@ValidationType_ID int = NULL,
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
/****** Object:  StoredProcedure [dbo].[v_RunAdd]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[v_RunAdd] (@project_k int, @runName nchar(500), @RunDate datetime = null )
--exec v_RunAdd 1, 'Test1' , '20200101'
AS
BEGIN
if @RunDate is null
select @RunDate = getdate()


INSERT INTO [dbo].[v_Run]
           ([Project_K],[RunName],[RunDate])
     VALUES
           (@project_k,@runName,@RunDate)

 END
GO
/****** Object:  StoredProcedure [dbo].[v_RunDo]    Script Date: 6/1/2021 2:30:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  procedure [dbo].[v_RunDo] (@project_k int, @runName nchar(500)='No Name Sent')
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
