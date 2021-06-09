select * from masteroperation
--INSERT [dbo].[MasterOperation] ([MasterOperationDescription], [MasterOperationType], [MasterOperationCode], [Category], [Status], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate], [FirmFunctionalAreaID], [DisplayName]) VALUES (N'BankDetails', N'Non-Transaction', N'BankDetails', N'BankDetails', 1, N'dbo', CAST(N'2021-05-26 10:51:11.130' AS DateTime), N'dbo', CAST(N'2021-05-26 10:51:11.130' AS DateTime), 9, N'View Bank Details')
select * from MenuGrouping
--INSERT [dbo].[MenuGrouping] ([GroupName], [MasterOperationId], [FirmFunctionalAreaID], [Path], [Page], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'Reports', 406, 9, N'/Reports/ViewBankReport', NULL, NULL, NULL, NULL, NULL)
--Insert in to spgetmenu procedure in reports
--WHEN M.MasterOperationID IN (406) THEN 'View Bank Branch'