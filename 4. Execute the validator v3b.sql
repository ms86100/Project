--1. Error machine process

USE [c_config]
GO
--step 1. add project (project name and description)
exec v_ProjectAdd 'Validate Owner API', ''
--select * from [dbo].[v_Project]

--step 2 add rules
exec dbo.v_RuleAdd @Project_K=6
	,@RuleName='SSN Length=9'-- ********
	,@RuleDesc='SSN must be 9 digits'
	,@RuleType_ID=1-- 1 file issue, 2= record, 3=field, 4 warning
	,@Section='Data' --way to group the rules
	,@RuleOrder=null
	-----IF SQL  fill in next 2 parameters
	--,@RuleHeaderSQL='select person_k, MaritalStatus_ID, null, null '--header  *******
	--,@RuleBodySQL='	from dw_Person
	--				where not(MaritalStatus_ID in (select m_k from c_config.dbo.map_item where mt_k=24)), ' --body  ***********
	----if using rules engine fill in below
	,@ValidationType_ID=1916 --"Field length                                      "
	,@Option1='Person_Link'
	,@Option2='htest.dbo.[ADP_Person]'
	,@Option3='SSN'
	,@Option4='rn'--field name of the rownumber
	,@Option5='9'
--	,@option6='Person_k'
/*
--FIE Steps:
exec v_RunDo 6, 'FIE run'

select sum(isnull(errors,0)) ct
from vv_Error_Summary
where run_k=(select max(run_k) from v_Run where project_k='6')
 and errortype='Severe'
 and isnull(errors,0)>0

 if count is >0 then fail gracefully. "Validation failed: Severe error count: "+ct
 if count=0 then log success.
 */

--STEP 3
--run a 
--param1= project_k, param2=Run Name
exec v_RunDo 6, 'FIE run'

--summary report
select Section,RuleName,RuleDesc,ErrorType,isnull(errors,0) [Error Count],Sample_Source_Link1 [Sample Key1],Sample_Source_Link2 [Sample Key2],Sample_SourceRowNum1 [Sample Row Number1],Sample_SourceRowNum2 [Sample Row Number2]
from vv_Error_Summary
where run_k=(select max(run_k) from v_Run where project_k='6')
 and errortype='Severe'
 and errorcount>0
Order by RuleType_ID, RuleOrder, Rule_K


--details
select *
from vv_ErrorDetails
where run_k=(select max(run_k) from v_Run where project_k='4')


SELECT Contact_K [Source_k],ContactType_ID [SourceValue],null [SourceValue2],null [SourceRowNum] , 178, 100 
from [dbo].[dw_Contact] s
			where isnull(ContactType_ID,'')='' 

--TESTING PASTE QUERY BELOW

SELECT Contact_K [Source_k],EffectiveFrom [SourceValue],null [SourceValue2], NULL  [SourceRowNum] , 183, 103 from [dbo].[dw_Contact] s
			where isnull(EffectiveFrom,'1/1/2020')='' 