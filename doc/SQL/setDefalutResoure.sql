 
 ---设置默认省市区
update [dbo].[LocaleStringResource]  set ResourceValue='贵州省' where ResourceName='Address.SelectState'

update [dbo].[LocaleStringResource]  set ResourceValue='其他' where ResourceName='Address.OtherNonUS'

if exists (select * from  [dbo].[LocaleStringResource] where  ResourceName ='Address.SelectCity' )
begin
  update [dbo].[LocaleStringResource] set ResourceValue='铜仁地区' where ResourceName='Address.SelectCity'
end 
else
begin
   insert  [dbo].[LocaleStringResource] (LanguageId ,ResourceName,ResourceValue) values(2,'Address.SelectCity','铜仁地区')  
end
 
 if exists (select * from  [dbo].[LocaleStringResource] where  ResourceName ='Address.SelectCounty' )
begin
  update [dbo].[LocaleStringResource] set ResourceValue='思南县' where ResourceName='Address.SelectCounty'
end 
else
begin
   insert  [dbo].[LocaleStringResource] (LanguageId ,ResourceName,ResourceValue) values(2,'Address.SelectCounty','思南县')  
end
 

update Setting set value='True' where Name='customersettings.hidedownloadableproductstab' ---下载商品
or name ='customersettings.hidebackinstocksubscriptionstab'  --到货订阅



update Setting set value='Flase' where name='rewardpointssettings.enabled' --禁用 我的积分  

update LocaleStringResource set ResourceValue='退货原因'   where ResourceName='Account.CustomerReturnRequests.Reason'
 