 
 ---����Ĭ��ʡ����
update [dbo].[LocaleStringResource]  set ResourceValue='����ʡ' where ResourceName='Address.SelectState'

update [dbo].[LocaleStringResource]  set ResourceValue='����' where ResourceName='Address.OtherNonUS'

if exists (select * from  [dbo].[LocaleStringResource] where  ResourceName ='Address.SelectCity' )
begin
  update [dbo].[LocaleStringResource] set ResourceValue='ͭ�ʵ���' where ResourceName='Address.SelectCity'
end 
else
begin
   insert  [dbo].[LocaleStringResource] (LanguageId ,ResourceName,ResourceValue) values(2,'Address.SelectCity','ͭ�ʵ���')  
end
 
 if exists (select * from  [dbo].[LocaleStringResource] where  ResourceName ='Address.SelectCounty' )
begin
  update [dbo].[LocaleStringResource] set ResourceValue='˼����' where ResourceName='Address.SelectCounty'
end 
else
begin
   insert  [dbo].[LocaleStringResource] (LanguageId ,ResourceName,ResourceValue) values(2,'Address.SelectCounty','˼����')  
end
 

update Setting set value='True' where Name='customersettings.hidedownloadableproductstab' ---������Ʒ
or name ='customersettings.hidebackinstocksubscriptionstab'  --��������



update Setting set value='Flase' where name='rewardpointssettings.enabled' --���� �ҵĻ���  

update LocaleStringResource set ResourceValue='�˻�ԭ��'   where ResourceName='Account.CustomerReturnRequests.Reason'
 


--�������ͷ�ʽ
update Setting set Value='True' where name ='shippingsettings.estimateshippingenabled'

 
 ---ִ�й�����Ҫִ��
 /*
 set identity_insert [Setting] ON--��
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (165, N'addresssettings.companyenabled', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (166, N'addresssettings.companyrequired', N'False', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (167, N'addresssettings.streetaddressenabled', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (168, N'addresssettings.streetaddressrequired', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (169, N'addresssettings.streetaddress2enabled', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (170, N'addresssettings.streetaddress2required', N'False', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (171, N'addresssettings.zippostalcodeenabled', N'True', 0)
--INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (172, N'addresssettings.zippostalcoderequired', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (173, N'addresssettings.cityenabled', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (174, N'addresssettings.cityrequired', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (175, N'addresssettings.countryenabled', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (176, N'addresssettings.stateprovinceenabled', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (177, N'addresssettings.phoneenabled', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (178, N'addresssettings.phonerequired', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (179, N'addresssettings.faxenabled', N'True', 0)
INSERT [dbo].[Setting] ([Id], [Name], [Value], [StoreId]) VALUES (180, N'addresssettings.faxrequired', N'False', 0)
set identity_insert [Setting] OFF

*/