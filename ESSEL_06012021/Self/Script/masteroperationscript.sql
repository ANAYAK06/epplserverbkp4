GO
INSERT [dbo].[MasterOperation] ([MasterOperationDescription], [MasterOperationType], [MasterOperationCode], [Category], [Status], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate], [FirmFunctionalAreaID], [DisplayName]) VALUES (N'AccruedConfiguration', N'Non-Transaction', N'AccruedConfiguration', N'AccruedConfiguration', 1, N'dbo', CAST(N'2021-01-06 09:22:02.137' AS DateTime), N'dbo', CAST(N'2021-01-06 09:22:02.137' AS DateTime), 10, N'Accrued Interest Configuration')
GO
INSERT [dbo].[MasterOperation] ([MasterOperationDescription], [MasterOperationType], [MasterOperationCode], [Category], [Status], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate], [FirmFunctionalAreaID], [DisplayName]) VALUES (N'OverHeadPercentageConfiguration', N'Non-Transaction', N'OverHeadPercentageConfiguration', N'OverHeadPercentageConfiguration', 1, N'dbo', CAST(N'2021-01-06 09:23:31.527' AS DateTime), N'dbo', CAST(N'2021-01-06 09:23:31.527' AS DateTime), 10, N'OverHead Percentage Configuration')
GO
INSERT [dbo].[MasterOperation] ([MasterOperationDescription], [MasterOperationType], [MasterOperationCode], [Category], [Status], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate], [FirmFunctionalAreaID], [DisplayName]) VALUES (N'DepreciationConfiguration', N'Non-Transaction', N'DepreciationConfiguration', N'DepreciationConfiguration', 1, N'dbo', CAST(N'2021-01-06 09:24:24.780' AS DateTime), N'dbo', CAST(N'2021-01-06 09:24:24.780' AS DateTime), 10, N'Depreciation Configuration')


--GO
--INSERT [dbo].[MenuGrouping] ([GroupName], [MasterOperationId], [FirmFunctionalAreaID], [Path], [Page], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'Master Configuration', 354, 10, N'/Home/AccruedInterestConfig', NULL, NULL, NULL, NULL, NULL)
--GO
--INSERT [dbo].[MenuGrouping] ([GroupName], [MasterOperationId], [FirmFunctionalAreaID], [Path], [Page], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'Master Configuration', 356, 10, N'/Home/OverHeadPercentageConfig', NULL, NULL, NULL, NULL, NULL)
--GO
--INSERT [dbo].[MenuGrouping] ([GroupName], [MasterOperationId], [FirmFunctionalAreaID], [Path], [Page], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'Master Configuration', 358, 10, N'/Home/DepreciationConfig', NULL, NULL, NULL, NULL, NULL)


select * from MasterOperation