/*
USAGE:

declare @theLog bigint
Exec @theLog=c_logStart @process='FIE',@logrunname='Import Owner API', @logend=null, @logstatus='Started', @logresult='inprogress'
print @theLog

--if success
Exec c_logAdd @logh_k=@theLog, @CountTable='c_HRPR.dbo.dw_Person', @logstep ='mystep1', @logresult='success'

--Trapped for errors and then report if found:
Exec c_logAdd @logh_k=@theLog, @CountTable='c_HRPR.dbo.dw_Person', @logstep ='mystep2', @logresult='failure', @lognote='Missing parameter1'


EXEC c_logEnd @logh_k=@TheLog,  @logresult='Failure'

select *
from vLogd
ORDER BY logh_k desc, logd_k asc
*/

--c_log
USE [c_config]
GO
Alter procedure c_logStart(@process nchar(10), @logrunname nchar(50)=null, @start datetime=null, @logend datetime=null, @logstatus nchar(10)='Started'
	, @logresult nchar(10)='inprogress')
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

		   print @process
		   Return @@identity
GO



alter procedure c_logAdd(@logh_k bigint,  @resCount decimal(18,2)=null, @CountTable varchar(500)=null, @logstep varchar(30) , @logresult varchar(10)='inprogress', @lognote varchar(500)=null)
--SIMPLE: exec c_logAdd 'SOS'
--one set: Exec c_logAdd @process='SOS', @logstatus='done'
--Should use: Exec c_logAdd @process='SOS', @logend=null, @logstatus='Started', @logresult='inprogress'--note cant use getdate function to send to a procedure
AS
declare @TheSQL nvarchar(1000)=''

if @CountTable>''
BEGIN
	set @TheSQL= 'select @resCountout=count(*) FROM '+@CountTable
	--print @TheSQL
	EXECUTE SP_EXECUTESQL @TheSQL,N'@resCountout decimal(18,2) OUTPUT', @resCountout=@resCount OUTPUT
END

INSERT INTO [dbo].[c_logd]
           ([logh_k]
           ,[logtime]
           ,[resCount]
           ,[logstep]
           ,[logdresult]
		   ,[logNote])
     
     VALUES
           (@logh_k
           , getdate()
           , @resCount
           ,@logstep
           , @logresult
		   ,@lognote)

		   print @logstep
		   Return @@identity
GO


ALTER procedure c_logEnd(@logh_k bigint,   @logresult varchar(10))
AS
BEGIN


UPDATE [dbo].[c_logh]
   SET [logend] = getdate()
      ,[logstatus] = 'done'
      ,[logresult] = @logresult
 WHERE logh_k=@logh_k
 
END
GO

alter view vLogh AS
select --h.*,
	h.[logh_k],p.mt_to LogProcess, LogRunName, s.mt_to LogStatus,logstart, logend, s.mt_to logstatus_desc, r.mt_to logresult_desc, 
	--time( logend-logstart) Duration
	datediff(SECOND,logstart, logend)*1.00/60 DurationMins--,*
from c_logh h
	left outer join map_item p on p.mt_k=6 and p.mt_from=h.logprocess
	left outer join map_item s on s.mt_k=7 and s.mt_from=h.logstatus
	left outer join map_item r on r.mt_k=8 and r.mt_from=h.logresult
GO
--truncate table dbo.c_logh
select * from vLogh
ORDER BY logstart desc

select *
from c_logh h
ORDER BY logstart desc


GO
ALTER view vLogd AS
select h.logh_k,logd_k,LogProcess,LogRunName,LogStatus,logstart,logend,DurationMins,logresult_desc,	logtime,logstep,resCount,logdresult,lognote
from vLogh h
  left outer join c_logd d on d.logh_k=h.logh_k
Go
select *
from vLogd
ORDER BY logh_k desc, logd_k asc

select * from map_item r where r.mt_k in (6,7,8)

