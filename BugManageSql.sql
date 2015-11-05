CREATE DATABASE BugManage
use BugManage
CREATE TABLE T_BugList
(
 F_ID INT IDENTITY(1,1) PRIMARY KEY,
 F_GID VARCHAR(40) DEFAULT NEWID(),
 F_BugName VARCHAR(100)  ,--bug名称
 F_Version VARCHAR(100)   ,--软件版本
 F_OS int  ,--系统类型 0 iOS 1Android 2其他
 F_Mobile  varchar (100)  ,--手机型号
 F_OSVersion VARCHAR(100),--系统版本
 F_Memo VARCHAR(500),--bug详情
 F_ImageList VARCHAR(500) DEFAULT '',--图片地址 用|隔开
 F_BugLevel int DEFAULT '1',--紧急程度 0一般 1紧急 2严重
 F_BugType [int]  ,--bug类型0appbug 1服务bug  2其他
 F_CreateName VARCHAR(100),----创建人
 F_STATUS CHAR(1) DEFAULT '1',--状态 1开启 0关闭  2其他
 F_CloseName VARCHAR(100),--关闭人
 F_CloseMemo VARCHAR(200) DEFAULT '',--关闭解释
 F_INDATE DATETIME DEFAULT GETDATE(),--创建时间
 F_EditDATE DATETIME DEFAULT GETDATE()----关闭时间
) 

SET IDENTITY_INSERT [ BugManage.[ owner.] ] { T_BugList } { OFF } 
 