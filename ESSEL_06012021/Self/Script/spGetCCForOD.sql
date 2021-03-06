ALTER procedure [dbo].[spGetCCForOD]  @Type VARCHAR(50)=null
AS
BEGIN
   IF(@Type is null)
	   BEGIN
			Select CC_Code,(CC_Code+' , '+CC_Name) CC_Name from Cost_Center where CC_Type='Performing' and CC_Status='Open' and Status='Approved' 
			GROUP by CC_Code,CC_Name ORDER BY CASE WHEN CC_Code='CCC' THEN CC_Code end ASC ,CASE WHEN CC_Code!='CCC' 
			THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC
	   END
   ELSE
	   BEGIN
			Select CC_Code,(CC_Code+' , '+CC_Name) CC_Name from Cost_Center where CC_Status='Open' and Status='Approved' 
			and CC_Code not in (Select CCCode from CCAccruedValues where ReportType=@Type)
			GROUP by CC_Code,CC_Name ORDER BY CASE WHEN CC_Code='CCC' THEN CC_Code end ASC ,CASE WHEN CC_Code!='CCC' 
			THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC
	   END
END


