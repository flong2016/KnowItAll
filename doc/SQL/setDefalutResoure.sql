 
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
 

  
 