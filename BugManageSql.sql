CREATE DATABASE BugManage
use BugManage
CREATE TABLE T_BugList
(
 F_ID INT IDENTITY(1,1) PRIMARY KEY,
 F_GID VARCHAR(40) DEFAULT NEWID(),
 F_BugName VARCHAR(100)  ,--bug����
 F_Version VARCHAR(100)   ,--����汾
 F_OS int  ,--ϵͳ���� 0 iOS 1Android 2����
 F_Mobile  varchar (100)  ,--�ֻ��ͺ�
 F_OSVersion VARCHAR(100),--ϵͳ�汾
 F_Memo VARCHAR(500),--bug����
 F_ImageList VARCHAR(500) DEFAULT '',--ͼƬ��ַ ��|����
 F_BugLevel int DEFAULT '1',--�����̶� 0һ�� 1���� 2����
 F_BugType [int]  ,--bug����0appbug 1����bug  2����
 F_CreateName VARCHAR(100),----������
 F_STATUS CHAR(1) DEFAULT '1',--״̬ 1���� 0�ر�  2����
 F_CloseName VARCHAR(100),--�ر���
 F_CloseMemo VARCHAR(200) DEFAULT '',--�رս���
 F_INDATE DATETIME DEFAULT GETDATE(),--����ʱ��
 F_EditDATE DATETIME DEFAULT GETDATE()----�ر�ʱ��
) 

SET IDENTITY_INSERT [ BugManage.[ owner.] ] { T_BugList } { OFF } 
 