-- MySQL dump 10.13  Distrib 5.7.12, for osx10.11 (x86_64)
--
-- Host: 127.0.0.1    Database: XMISCM_MYSQL
-- ------------------------------------------------------
-- Server version	5.7.12

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Dept`
--

DROP TABLE IF EXISTS `Dept`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Dept` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Dept`
--

LOCK TABLES `Dept` WRITE;
/*!40000 ALTER TABLE `Dept` DISABLE KEYS */;
INSERT INTO `Dept` (`Id`, `PId`, `Name`) VALUES (1,0,'部门'),(6,1,'总经办'),(7,1,'市场部'),(8,1,'财务部'),(9,1,'采购部'),(10,6,'总经理'),(11,0,'43434343'),(12,1,'额威威'),(13,12,'55555'),(14,13,'55555555'),(15,11,'5444'),(16,15,'32323'),(17,6,'333333'),(18,17,'4444444444'),(19,18,'1'),(20,7,'2222'),(21,20,'33'),(22,18,'3333'),(23,0,'谔谔谔谔');
/*!40000 ALTER TABLE `Dept` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `LeftMenuPermissions`
--

DROP TABLE IF EXISTS `LeftMenuPermissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `LeftMenuPermissions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LeftMenuId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  `PermissionsId` int(11) NOT NULL,
  `Have` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=155 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `LeftMenuPermissions`
--

LOCK TABLES `LeftMenuPermissions` WRITE;
/*!40000 ALTER TABLE `LeftMenuPermissions` DISABLE KEYS */;
INSERT INTO `LeftMenuPermissions` (`Id`, `LeftMenuId`, `RoleId`, `PermissionsId`, `Have`) VALUES (1,6,4,6,1),(2,4,4,1,1),(3,7,4,6,1),(4,5,4,4,1),(5,1,1,6,0),(6,2,1,6,0),(7,1,1,1,0),(8,1,1,3,0),(9,1,1,4,0),(10,2,2,6,1),(11,1,1,5,0),(12,1,1,7,1),(13,5,1,4,1),(14,4,1,3,1),(15,3,1,1,0),(16,6,1,5,0),(17,7,1,7,1),(18,7,1,6,0),(19,8,1,1,0),(20,9,1,3,0),(21,10,1,4,0),(22,11,1,5,1),(23,12,1,7,0),(24,45,1,5,0),(25,45,1,6,1),(26,45,1,1,1),(27,45,1,3,1),(28,45,1,4,1),(30,3,1,6,0),(31,4,1,6,0),(32,5,1,6,0),(33,6,1,6,0),(34,8,1,6,0),(35,9,1,6,0),(36,10,1,6,0),(37,2,1,3,0),(38,3,1,4,0),(39,4,1,5,1),(40,5,1,7,1),(41,4,1,4,1),(42,4,1,1,1),(44,21,3,4,1),(51,1,1,24,1),(52,2,1,24,1),(53,3,1,24,1),(54,4,1,24,1),(55,5,1,24,1),(56,6,1,24,1),(57,7,1,24,1),(58,9,1,24,1),(59,8,1,24,1),(60,10,1,24,1),(61,11,1,24,1),(62,12,1,24,1),(63,13,1,24,1),(64,14,1,24,1),(65,15,1,24,1),(66,16,1,24,1),(67,17,1,24,1),(68,18,1,24,1),(71,2,1,7,1),(72,3,1,7,1),(73,4,1,7,1),(74,6,1,7,1),(75,8,1,7,1),(76,9,1,7,1),(77,10,1,7,1),(78,11,1,6,0),(79,12,1,6,0),(80,13,1,6,0),(81,14,1,6,0),(82,15,1,6,0),(83,16,1,6,0),(84,17,1,6,0),(85,19,1,6,0),(86,18,1,6,0),(87,20,1,6,0),(88,21,1,6,0),(89,22,1,6,0),(90,23,1,6,0),(91,24,1,6,0),(92,25,1,6,0),(93,27,1,6,0),(94,26,1,6,0),(95,45,2,6,1),(96,46,2,6,1),(97,36,2,6,1),(98,46,1,6,1),(99,36,1,6,1),(100,34,1,6,1),(101,33,1,6,1),(102,7,5,6,0),(103,7,5,1,0),(104,28,1,6,0),(105,29,1,6,0),(106,30,1,6,0),(107,31,1,6,0),(108,32,1,6,0),(109,35,1,6,1),(110,37,1,6,1),(111,38,1,6,1),(112,44,1,6,0),(113,47,1,6,1),(114,48,1,6,1),(115,49,1,6,1),(116,50,1,6,1),(117,51,1,6,1),(118,52,1,6,0),(119,2,1,10,1),(120,9,1,1,1),(121,53,1,6,1),(122,54,1,6,1),(123,55,1,6,1),(124,56,1,6,1),(125,57,1,6,1),(126,34,1,1,0),(127,34,1,3,1),(128,34,1,4,1),(129,34,1,5,1),(130,34,1,7,0),(131,53,1,1,0),(132,53,1,3,0),(133,53,1,4,0),(134,36,1,1,0),(135,36,1,3,0),(136,36,1,4,0),(137,36,1,5,0),(138,36,1,7,1),(139,33,1,1,1),(140,33,1,3,1),(141,33,1,4,0),(142,37,1,3,1),(143,54,1,1,1),(144,54,1,3,1),(145,54,1,4,1),(146,60,1,6,0),(147,63,1,6,0),(148,5,1,3,1),(149,64,1,6,0),(150,56,1,4,1),(151,56,1,1,1),(152,65,1,6,1),(153,65,1,3,1),(154,56,1,3,1);
/*!40000 ALTER TABLE `LeftMenuPermissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `LeftMenus`
--

DROP TABLE IF EXISTS `LeftMenus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `LeftMenus` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DisplayName` varchar(100) NOT NULL COMMENT '菜单名称',
  `Ico` varchar(100) DEFAULT NULL COMMENT '图标',
  `ParentId` int(11) NOT NULL COMMENT '父级编号Id',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `MenuId` varchar(30) DEFAULT NULL COMMENT '菜单与 dll 一一对应的窗口编号',
  `NavBarGroupId` int(11) NOT NULL COMMENT '功能组编号',
  `DllPath` varchar(100) DEFAULT NULL COMMENT 'dll 路径',
  `EntryFunction` varchar(100) DEFAULT NULL COMMENT '入口函数',
  `StartWithSys` bit(1) NOT NULL DEFAULT b'0',
  `Timestamp` bigint(20) NOT NULL COMMENT '时间戳',
  `Sys` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否是系统级的菜单',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `LeftMenus`
--

LOCK TABLES `LeftMenus` WRITE;
/*!40000 ALTER TABLE `LeftMenus` DISABLE KEYS */;
INSERT INTO `LeftMenus` (`Id`, `DisplayName`, `Ico`, `ParentId`, `Sort`, `MenuId`, `NavBarGroupId`, `DllPath`, `EntryFunction`, `StartWithSys`, `Timestamp`, `Sys`) VALUES (1,'内部公告',NULL,0,4,'呃呃呃',1,'ClassLibrary1.dll','ClassLibrary1.Class1','\0',1487086443,'\0'),(2,'00000',NULL,0,1,'1',1,'11','11','\0',1487086443,'\0'),(3,'我发送的短信',NULL,2,1,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(4,'短信群发',NULL,2,999999,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(5,'短信发送记录',NULL,2,2,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(6,'事物管理',NULL,0,5,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(7,'待办事物',NULL,6,0,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(8,'已办事物',NULL,6,0,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(9,'下属事物',NULL,6,0,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(10,'审批管理',NULL,0,3,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(11,'我的申请',NULL,0,7,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(12,'经我审批',NULL,10,0,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(13,'文档管理',NULL,0,6,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(14,'私有文档',NULL,13,0,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(15,'公有文档',NULL,13,0,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(16,'任务管理',NULL,0,8,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(17,'我创建的',NULL,16,0,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(18,'分配给我的',NULL,16,0,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(19,'客户资料',NULL,0,9,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(20,'联系人',NULL,0,2,'呃呃呃得到',1,'C:\\PersonalProject\\FrameSystem\\Frame.Utils\\bin\\Debug\\Frame.Utils.dll','Frame.Utils.Functions','\0',1487086443,'\0'),(21,'职员档案管理',NULL,0,10,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(22,'收支管理',NULL,0,11,NULL,1,NULL,NULL,'\0',1487086443,'\0'),(23,'新增客户',NULL,0,1,NULL,2,NULL,NULL,'\0',1487155563,'\0'),(24,'客户资料',NULL,0,4,NULL,2,NULL,NULL,'\0',1487155563,'\0'),(25,'联系人',NULL,0,2,NULL,2,NULL,NULL,'\0',1487155563,'\0'),(26,'客户跟进',NULL,0,3,NULL,2,NULL,NULL,'\0',1487155563,'\0'),(27,'跟进提醒',NULL,26,0,NULL,2,NULL,NULL,'\0',1487155563,'\0'),(28,'客户合同',NULL,0,5,NULL,2,NULL,NULL,'\0',1487155563,'\0'),(29,'合同到期提醒',NULL,0,6,NULL,2,NULL,NULL,'\0',1487155563,'\0'),(30,'客户分布',NULL,0,7,NULL,2,NULL,NULL,'\0',1487155563,'\0'),(31,'客户结算',NULL,0,8,NULL,2,NULL,NULL,'\0',1487155563,'\0'),(32,'系统配置',NULL,0,2,NULL,3,NULL,NULL,'\0',1487350598,'\0'),(33,'操作员管理',NULL,0,3,'操作员管理',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487350598,'\0'),(34,'员工档案',NULL,0,4,'用户管理',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487350598,'\0'),(36,'登陆日志管理',NULL,0,6,'登陆日志管理',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487350598,'\0'),(37,'公司信息',NULL,0,7,'公司信息设置',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487350598,'\0'),(44,'顶顶顶',NULL,20,99999,'权限管理000',1,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487086443,'\0'),(45,'权限管理',NULL,0,8,'权限管理',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487350598,'\0'),(51,'修改密码',NULL,0,9,'修改密码',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487350598,'\0'),(52,'系统初始化',NULL,0,10,'系统初始化',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487350598,'\0'),(53,'部门管理',NULL,0,11,'部门管理',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','',1487350598,'\0'),(54,'左侧菜单管理',NULL,0,12,'左侧菜单管理',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','',1487350598,''),(55,'顶部菜单管理',NULL,0,13,'顶部菜单管理',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487350598,'\0'),(56,'功能组管理',NULL,0,14,'功能组管理',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487350598,'\0'),(60,'嗡嗡嗡',NULL,23,15,'',2,'','','\0',1487155563,'\0'),(63,'6666',NULL,60,16,'',2,'','','\0',1487155563,'\0'),(64,'登陆主题设置',NULL,0,15,'登陆主题设置',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487367556,'\0'),(65,'Banner 设置',NULL,0,16,'Banner 设置',3,'SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControl','\0',1487524385,'\0');
/*!40000 ALTER TABLE `LeftMenus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Log`
--

DROP TABLE IF EXISTS `Log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Log` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LoginName` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `LoginTime` datetime DEFAULT NULL,
  `LoginRole` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `LoginMach` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `LoginCpu` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=290 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Log`
--

LOCK TABLES `Log` WRITE;
/*!40000 ALTER TABLE `Log` DISABLE KEYS */;
INSERT INTO `Log` (`Id`, `LoginName`, `LoginTime`, `LoginRole`, `LoginMach`, `LoginCpu`) VALUES (3,'admin','2017-01-11 17:11:57','管理员','APPLE1794','BFEBFBFF000306D4'),(4,'admin','2017-01-11 17:15:37','管理员','APPLE1794','BFEBFBFF000306D4'),(5,'admin','2017-01-11 17:16:47','管理员','APPLE1794','BFEBFBFF000306D4'),(6,'admin','2017-01-11 17:18:38','管理员','APPLE1794','BFEBFBFF000306D4'),(7,'admin','2017-01-11 20:37:24','管理员','APPLE1794','BFEBFBFF000306D4'),(8,'admin','2017-01-11 20:55:30','管理员','APPLE1794','BFEBFBFF000306D4'),(9,'admin','2017-01-11 21:02:38','管理员','APPLE1794','BFEBFBFF000306D4'),(10,'admin','2017-01-11 21:06:57','管理员','APPLE1794','BFEBFBFF000306D4'),(11,'admin','2017-01-11 21:10:58','管理员','APPLE1794','BFEBFBFF000306D4'),(12,'admin','2017-01-11 22:01:05','管理员','APPLE1794','BFEBFBFF000306D4'),(13,'admin','2017-01-11 22:02:42','管理员','APPLE1794','BFEBFBFF000306D4'),(14,'admin','2017-01-11 22:05:10','管理员','APPLE1794','BFEBFBFF000306D4'),(15,'admin','2017-01-11 22:09:05','管理员','APPLE1794','BFEBFBFF000306D4'),(16,'admin','2017-01-11 22:11:40','管理员','APPLE1794','BFEBFBFF000306D4'),(17,'admin','2017-01-12 08:41:35','管理员','APPLE1794','BFEBFBFF000306D4'),(18,'admin','2017-01-12 08:52:22','管理员','APPLE1794','BFEBFBFF000306D4'),(19,'admin','2017-01-12 09:07:18','管理员','APPLE1794','BFEBFBFF000306D4'),(20,'admin','2017-01-12 09:31:16','管理员','APPLE1794','BFEBFBFF000306D4'),(21,'admin','2017-01-12 13:02:29','管理员','APPLE1794','BFEBFBFF000306D4'),(22,'admin','2017-01-12 13:04:06','管理员','APPLE1794','BFEBFBFF000306D4'),(23,'admin','2017-01-12 13:13:45','管理员','APPLE1794','BFEBFBFF000306D4'),(24,'admin','2017-01-12 13:54:09','管理员','APPLE1794','BFEBFBFF000306D4'),(25,'admin','2017-01-12 13:56:49','管理员','APPLE1794','BFEBFBFF000306D4'),(26,'admin','2017-01-12 13:57:48','管理员','APPLE1794','BFEBFBFF000306D4'),(27,'admin','2017-01-12 14:19:00','管理员','APPLE1794','BFEBFBFF000306D4'),(28,'admin','2017-01-12 16:29:20','管理员','APPLE1794','BFEBFBFF000306D4'),(29,'admin','2017-01-12 16:30:57','管理员','APPLE1794','BFEBFBFF000306D4'),(30,'admin','2017-01-12 16:38:52','管理员','APPLE1794','BFEBFBFF000306D4'),(31,'admin','2017-01-12 16:44:20','管理员','APPLE1794','BFEBFBFF000306D4'),(32,'admin','2017-01-12 16:49:16','管理员','APPLE1794','BFEBFBFF000306D4'),(33,'admin','2017-01-12 16:57:38','管理员','APPLE1794','BFEBFBFF000306D4'),(34,'admin','2017-01-12 16:59:31','管理员','APPLE1794','BFEBFBFF000306D4'),(35,'admin','2017-01-13 15:43:41','管理员','APPLE1794','BFEBFBFF000306D4'),(36,'admin','2017-01-13 15:47:53','管理员','APPLE1794','BFEBFBFF000306D4'),(37,'admin','2017-01-13 15:58:06','管理员','APPLE1794','BFEBFBFF000306D4'),(38,'admin','2017-01-13 15:59:15','管理员','APPLE1794','BFEBFBFF000306D4'),(39,'admin','2017-01-13 16:02:51','管理员','APPLE1794','BFEBFBFF000306D4'),(40,'admin','2017-01-13 16:06:39','管理员','APPLE1794','BFEBFBFF000306D4'),(41,'admin','2017-01-16 10:24:50','管理员','APPLE1794','BFEBFBFF000306D4'),(42,'admin','2017-01-16 10:27:10','管理员','APPLE1794','BFEBFBFF000306D4'),(43,'admin','2017-01-16 10:53:28','管理员','APPLE1794','BFEBFBFF000306D4'),(44,'admin','2017-01-16 10:58:46','管理员','APPLE1794','BFEBFBFF000306D4'),(45,'admin','2017-01-16 11:07:56','管理员','APPLE1794','BFEBFBFF000306D4'),(46,'admin','2017-01-16 11:09:21','管理员','APPLE1794','BFEBFBFF000306D4'),(47,'admin','2017-01-16 11:10:53','管理员','APPLE1794','BFEBFBFF000306D4'),(48,'admin','2017-01-16 11:13:16','管理员','APPLE1794','BFEBFBFF000306D4'),(49,'admin','2017-01-16 11:16:06','管理员','APPLE1794','BFEBFBFF000306D4'),(50,'admin','2017-01-17 14:22:37','管理员','APPLE1794','BFEBFBFF000306D4'),(51,'admin','2017-01-19 13:47:05','管理员','APPLE1794','BFEBFBFF000306D4'),(52,'admin','2017-01-19 13:49:55','管理员','APPLE1794','BFEBFBFF000306D4'),(53,'admin','2017-01-19 16:12:17','管理员','APPLE1794','BFEBFBFF000306D4'),(54,'admin','2017-01-19 16:15:44','管理员','APPLE1794','BFEBFBFF000306D4'),(55,'admin','2017-01-19 16:20:22','管理员','APPLE1794','BFEBFBFF000306D4'),(56,'admin','2017-01-19 16:34:57','管理员','APPLE1794','BFEBFBFF000306D4'),(57,'admin','2017-01-19 16:36:40','管理员','APPLE1794','BFEBFBFF000306D4'),(58,'admin','2017-01-19 16:38:48','管理员','APPLE1794','BFEBFBFF000306D4'),(59,'admin','2017-01-20 08:55:05','管理员','APPLE1794','BFEBFBFF000306D4'),(60,'admin','2017-01-20 15:50:53','管理员','APPLE1794','BFEBFBFF000306D4'),(61,'admin','2017-01-20 15:58:20','管理员','APPLE1794','BFEBFBFF000306D4'),(62,'admin','2017-01-20 16:07:03','管理员','APPLE1794','BFEBFBFF000306D4'),(63,'admin','2017-01-20 16:08:22','管理员','APPLE1794','BFEBFBFF000306D4'),(64,'admin','2017-01-20 16:38:26','管理员','APPLE1794','BFEBFBFF000306D4'),(65,'admin','2017-01-20 16:51:50','管理员','APPLE1794','BFEBFBFF000306D4'),(66,'admin','2017-01-21 09:55:00','管理员','APPLE1794','BFEBFBFF000306D4'),(67,'admin','2017-01-21 10:04:51','管理员','APPLE1794','BFEBFBFF000306D4'),(68,'admin','2017-01-21 10:15:02','管理员','APPLE1794','BFEBFBFF000306D4'),(69,'admin','2017-01-21 10:36:09','管理员','APPLE1794','BFEBFBFF000306D4'),(70,'admin','2017-01-21 10:50:40','管理员','APPLE1794','BFEBFBFF000306D4'),(71,'admin','2017-01-21 11:04:28','管理员','APPLE1794','BFEBFBFF000306D4'),(72,'admin','2017-01-21 11:16:53','管理员','APPLE1794','BFEBFBFF000306D4'),(73,'admin','2017-01-21 11:23:48','管理员','APPLE1794','BFEBFBFF000306D4'),(74,'admin','2017-01-21 11:25:24','管理员','APPLE1794','BFEBFBFF000306D4'),(75,'admin','2017-01-21 11:40:19','管理员','APPLE1794','BFEBFBFF000306D4'),(76,'admin','2017-01-21 11:50:14','管理员','APPLE1794','BFEBFBFF000306D4'),(77,'admin','2017-01-21 12:12:50','管理员','APPLE1794','BFEBFBFF000306D4'),(78,'admin','2017-01-21 12:18:14','管理员','APPLE1794','BFEBFBFF000306D4'),(79,'admin','2017-01-21 12:24:35','管理员','APPLE1794','BFEBFBFF000306D4'),(80,'admin','2017-01-21 12:27:10','管理员','APPLE1794','BFEBFBFF000306D4'),(81,'admin','2017-01-21 12:45:11','管理员','APPLE1794','BFEBFBFF000306D4'),(82,'admin','2017-01-21 12:51:22','管理员','APPLE1794','BFEBFBFF000306D4'),(83,'admin','2017-01-21 13:05:39','管理员','APPLE1794','BFEBFBFF000306D4'),(84,'admin','2017-01-21 22:58:06','管理员','APPLE1794','BFEBFBFF000306D4'),(85,'admin','2017-01-22 21:06:54','管理员','APPLE1794','BFEBFBFF000306D4'),(86,'admin','2017-01-22 21:52:40','管理员','APPLE1794','BFEBFBFF000306D4'),(87,'admin','2017-01-22 22:05:26','管理员','APPLE1794','BFEBFBFF000306D4'),(88,'admin','2017-01-22 22:22:40','管理员','APPLE1794','BFEBFBFF000306D4'),(89,'admin','2017-01-23 12:47:27','管理员','APPLE1794','BFEBFBFF000306D4'),(90,'admin','2017-01-23 13:01:49','管理员','APPLE1794','BFEBFBFF000306D4'),(91,'admin','2017-01-23 13:36:13','管理员','APPLE1794','BFEBFBFF000306D4'),(92,'admin','2017-01-23 14:04:28','管理员','APPLE1794','BFEBFBFF000306D4'),(93,'admin','2017-01-23 14:23:56','管理员','APPLE1794','BFEBFBFF000306D4'),(94,'admin','2017-01-23 15:29:48','管理员','APPLE1794','BFEBFBFF000306D4'),(95,'admin','2017-01-23 15:35:47','管理员','APPLE1794','BFEBFBFF000306D4'),(96,'admin','2017-01-23 15:39:08','管理员','APPLE1794','BFEBFBFF000306D4'),(97,'admin','2017-01-23 16:04:54','管理员','APPLE1794','BFEBFBFF000306D4'),(98,'admin','2017-01-23 16:09:16','管理员','APPLE1794','BFEBFBFF000306D4'),(99,'admin','2017-01-23 16:25:41','管理员','APPLE1794','BFEBFBFF000306D4'),(100,'admin','2017-01-23 16:29:08','管理员','APPLE1794','BFEBFBFF000306D4'),(101,'admin','2017-01-23 16:33:24','管理员','APPLE1794','BFEBFBFF000306D4'),(102,'admin','2017-01-24 09:06:35','管理员','APPLE1794','BFEBFBFF000306D4'),(103,'admin','2017-01-24 09:32:58','管理员','APPLE1794','BFEBFBFF000306D4'),(104,'admin','2017-01-24 09:36:06','管理员','APPLE1794','BFEBFBFF000306D4'),(105,'admin','2017-01-24 09:40:18','管理员','APPLE1794','BFEBFBFF000306D4'),(106,'admin','2017-01-24 09:59:28','管理员','APPLE1794','BFEBFBFF000306D4'),(107,'admin','2017-01-24 10:39:22','管理员','APPLE1794','BFEBFBFF000306D4'),(108,'admin','2017-01-24 13:46:41','管理员','APPLE1794','BFEBFBFF000306D4'),(109,'admin','2017-01-24 14:07:31','管理员','APPLE1794','BFEBFBFF000306D4'),(110,'admin','2017-01-24 14:20:14','管理员','APPLE1794','BFEBFBFF000306D4'),(111,'admin','2017-01-24 14:25:16','管理员','APPLE1794','BFEBFBFF000306D4'),(112,'admin','2017-01-24 14:32:13','管理员','APPLE1794','BFEBFBFF000306D4'),(113,'admin','2017-01-24 14:33:27','管理员','APPLE1794','BFEBFBFF000306D4'),(114,'admin','2017-01-24 14:44:07','管理员','APPLE1794','BFEBFBFF000306D4'),(115,'admin','2017-01-24 15:01:07','管理员','APPLE1794','BFEBFBFF000306D4'),(116,'admin','2017-01-24 15:15:22','管理员','APPLE1794','BFEBFBFF000306D4'),(117,'admin','2017-01-24 15:43:50','管理员','APPLE1794','BFEBFBFF000306D4'),(118,'admin','2017-01-24 15:51:16','管理员','APPLE1794','BFEBFBFF000306D4'),(119,'admin','2017-02-04 09:40:57','管理员','APPLE1794','BFEBFBFF000306D4'),(120,'admin','2017-02-04 09:42:50','管理员','APPLE1794','BFEBFBFF000306D4'),(121,'admin','2017-02-04 09:45:00','管理员','APPLE1794','BFEBFBFF000306D4'),(122,'admin','2017-02-04 09:47:35','管理员','APPLE1794','BFEBFBFF000306D4'),(123,'admin','2017-02-04 09:50:19','管理员','APPLE1794','BFEBFBFF000306D4'),(124,'admin','2017-02-04 09:55:54','管理员','APPLE1794','BFEBFBFF000306D4'),(125,'admin','2017-02-05 19:53:31','管理员','APPLE1794','BFEBFBFF000306D4'),(126,'admin','2017-02-08 17:01:25','管理员','APPLE1794','BFEBFBFF000306D4'),(127,'admin','2017-02-08 17:04:18','管理员','APPLE1794','BFEBFBFF000306D4'),(128,'admin','2017-02-13 14:54:57','管理员','APPLE1794','BFEBFBFF000306D4'),(129,'admin','2017-02-13 15:12:58','管理员','APPLE1794','BFEBFBFF000306D4'),(130,'admin','2017-02-13 15:31:59','管理员','APPLE1794','BFEBFBFF000306D4'),(131,'admin','2017-02-13 16:09:26','管理员','APPLE1794','BFEBFBFF000306D4'),(132,'admin','2017-02-13 16:19:51','管理员','APPLE1794','BFEBFBFF000306D4'),(133,'admin','2017-02-13 16:23:34','管理员','APPLE1794','BFEBFBFF000306D4'),(134,'admin','2017-02-13 16:49:18','管理员','APPLE1794','BFEBFBFF000306D4'),(135,'admin','2017-02-13 16:57:12','管理员','APPLE1794','BFEBFBFF000306D4'),(136,'admin','2017-02-14 08:53:34','管理员','APPLE1794','BFEBFBFF000306D4'),(137,'admin','2017-02-14 08:59:15','管理员','APPLE1794','BFEBFBFF000306D4'),(138,'admin','2017-02-14 09:42:15','管理员','APPLE1794','BFEBFBFF000306D4'),(139,'admin','2017-02-14 09:44:09','财务部','APPLE1794','BFEBFBFF000306D4'),(140,'admin','2017-02-14 09:46:39','销售人员','APPLE1794','BFEBFBFF000306D4'),(141,'admin','2017-02-14 12:52:18','管理员','APPLE1794','BFEBFBFF000306D4'),(142,'admin','2017-02-14 13:36:22','管理员','APPLE1794','BFEBFBFF000306D4'),(143,'admin','2017-02-14 13:38:12','管理员','APPLE1794','BFEBFBFF000306D4'),(144,'admin','2017-02-14 13:43:09','管理员','APPLE1794','BFEBFBFF000306D4'),(145,'admin','2017-02-14 13:45:48','管理员','APPLE1794','BFEBFBFF000306D4'),(146,'admin','2017-02-14 13:54:53','管理员','APPLE1794','BFEBFBFF000306D4'),(147,'admin','2017-02-14 15:00:24','管理员','APPLE1794','BFEBFBFF000306D4'),(148,'admin','2017-02-14 15:04:47','管理员','APPLE1794','BFEBFBFF000306D4'),(149,'admin','2017-02-14 15:29:44','管理员','APPLE1794','BFEBFBFF000306D4'),(150,'admin','2017-02-14 15:31:29','管理员','APPLE1794','BFEBFBFF000306D4'),(151,'admin','2017-02-14 15:34:37','管理员','APPLE1794','BFEBFBFF000306D4'),(152,'admin','2017-02-14 16:39:29','管理员','APPLE1794','BFEBFBFF000306D4'),(153,'admin','2017-02-14 16:57:19','管理员','APPLE1794','BFEBFBFF000306D4'),(154,'admin','2017-02-14 16:59:15','管理员','APPLE1794','BFEBFBFF000306D4'),(155,'admin','2017-02-14 17:01:55','管理员','APPLE1794','BFEBFBFF000306D4'),(156,'admin','2017-02-14 17:03:06','管理员','APPLE1794','BFEBFBFF000306D4'),(157,'admin','2017-02-14 17:10:05','管理员','APPLE1794','BFEBFBFF000306D4'),(158,'admin','2017-02-14 21:15:38','管理员','APPLE1794','BFEBFBFF000306D4'),(159,'admin','2017-02-14 21:21:17','管理员','APPLE1794','BFEBFBFF000306D4'),(160,'admin','2017-02-14 21:29:21','管理员','APPLE1794','BFEBFBFF000306D4'),(161,'admin','2017-02-14 22:01:55','管理员','APPLE1794','BFEBFBFF000306D4'),(162,'admin','2017-02-14 22:05:51','管理员','APPLE1794','BFEBFBFF000306D4'),(163,'admin','2017-02-15 09:01:23','管理员','APPLE1794','BFEBFBFF000306D4'),(164,'admin','2017-02-15 09:02:56','管理员','APPLE1794','BFEBFBFF000306D4'),(165,'admin','2017-02-15 09:05:30','管理员','APPLE1794','BFEBFBFF000306D4'),(166,'admin','2017-02-15 10:30:27','管理员','APPLE1794','BFEBFBFF000306D4'),(167,'admin','2017-02-15 10:36:08','管理员','APPLE1794','BFEBFBFF000306D4'),(168,'admin','2017-02-15 10:44:40','管理员','APPLE1794','BFEBFBFF000306D4'),(169,'admin','2017-02-15 10:50:08','管理员','APPLE1794','BFEBFBFF000306D4'),(170,'admin','2017-02-15 10:51:25','管理员','APPLE1794','BFEBFBFF000306D4'),(171,'admin','2017-02-15 10:57:37','管理员','APPLE1794','BFEBFBFF000306D4'),(172,'admin','2017-02-15 11:04:32','管理员','APPLE1794','BFEBFBFF000306D4'),(173,'admin','2017-02-15 11:05:30','管理员','APPLE1794','BFEBFBFF000306D4'),(174,'admin','2017-02-15 11:18:59','管理员','APPLE1794','BFEBFBFF000306D4'),(175,'admin','2017-02-15 15:41:50','管理员','APPLE1794','BFEBFBFF000306D4'),(176,'admin','2017-02-16 21:25:58','管理员','APPLE1794','BFEBFBFF000306D4'),(177,'admin','2017-02-16 21:30:02','管理员','APPLE1794','BFEBFBFF000306D4'),(178,'admin','2017-02-16 21:34:26','管理员','APPLE1794','BFEBFBFF000306D4'),(179,'admin','2017-02-16 21:47:11','管理员','APPLE1794','BFEBFBFF000306D4'),(180,'admin','2017-02-16 21:49:48','管理员','APPLE1794','BFEBFBFF000306D4'),(181,'admin','2017-02-16 21:56:46','管理员','APPLE1794','BFEBFBFF000306D4'),(182,'admin','2017-02-16 22:00:27','管理员','APPLE1794','BFEBFBFF000306D4'),(183,'admin','2017-02-17 10:24:15','管理员','APPLE1794','BFEBFBFF000306D4'),(184,'admin','2017-02-17 10:34:07','管理员','APPLE1794','BFEBFBFF000306D4'),(185,'admin','2017-02-17 10:40:14','管理员','APPLE1794','BFEBFBFF000306D4'),(186,'admin','2017-02-17 10:44:15','管理员','APPLE1794','BFEBFBFF000306D4'),(187,'admin','2017-02-17 10:58:47','管理员','APPLE1794','BFEBFBFF000306D4'),(188,'admin','2017-02-17 11:08:01','管理员','APPLE1794','BFEBFBFF000306D4'),(189,'admin','2017-02-17 11:10:39','管理员','APPLE1794','BFEBFBFF000306D4'),(190,'admin','2017-02-17 11:11:49','管理员','APPLE1794','BFEBFBFF000306D4'),(191,'admin','2017-02-17 14:34:02','管理员','APPLE1794','BFEBFBFF000306D4'),(192,'admin','2017-02-17 14:35:34','管理员','APPLE1794','BFEBFBFF000306D4'),(193,'admin','2017-02-17 14:37:19','管理员','APPLE1794','BFEBFBFF000306D4'),(194,'admin','2017-02-17 14:40:01','管理员','APPLE1794','BFEBFBFF000306D4'),(195,'admin','2017-02-17 14:43:04','管理员','APPLE1794','BFEBFBFF000306D4'),(196,'admin','2017-02-17 14:45:51','管理员','APPLE1794','BFEBFBFF000306D4'),(197,'admin','2017-02-17 14:48:22','管理员','APPLE1794','BFEBFBFF000306D4'),(198,'admin','2017-02-17 14:50:14','管理员','APPLE1794','BFEBFBFF000306D4'),(199,'admin','2017-02-17 14:54:45','管理员','APPLE1794','BFEBFBFF000306D4'),(200,'admin','2017-02-17 14:58:03','管理员','APPLE1794','BFEBFBFF000306D4'),(201,'admin','2017-02-17 15:06:38','管理员','APPLE1794','BFEBFBFF000306D4'),(202,'admin','2017-02-17 15:07:36','管理员','APPLE1794','BFEBFBFF000306D4'),(203,'admin','2017-02-17 15:12:40','管理员','APPLE1794','BFEBFBFF000306D4'),(204,'admin','2017-02-17 15:13:44','管理员','APPLE1794','BFEBFBFF000306D4'),(205,'admin','2017-02-17 15:14:39','管理员','APPLE1794','BFEBFBFF000306D4'),(206,'admin','2017-02-17 15:15:09','管理员','APPLE1794','BFEBFBFF000306D4'),(207,'admin','2017-02-17 15:15:42','管理员','APPLE1794','BFEBFBFF000306D4'),(208,'admin','2017-02-17 15:16:49','管理员','APPLE1794','BFEBFBFF000306D4'),(209,'admin','2017-02-17 15:17:48','管理员','APPLE1794','BFEBFBFF000306D4'),(210,'admin','2017-02-17 15:19:22','管理员','APPLE1794','BFEBFBFF000306D4'),(211,'admin','2017-02-17 15:29:21','管理员','APPLE1794','BFEBFBFF000306D4'),(212,'admin','2017-02-17 15:33:19','管理员','APPLE1794','BFEBFBFF000306D4'),(213,'admin','2017-02-17 16:12:18','管理员','APPLE1794','BFEBFBFF000306D4'),(214,'admin','2017-02-17 16:32:58','管理员','APPLE1794','BFEBFBFF000306D4'),(215,'admin','2017-02-17 16:35:13','管理员','APPLE1794','BFEBFBFF000306D4'),(216,'admin','2017-02-17 16:38:37','管理员','APPLE1794','BFEBFBFF000306D4'),(217,'admin','2017-02-17 16:51:03','管理员','APPLE1794','BFEBFBFF000306D4'),(218,'admin','2017-02-17 16:55:26','管理员','APPLE1794','BFEBFBFF000306D4'),(219,'admin','2017-02-17 17:10:31','管理员','APPLE1794','BFEBFBFF000306D4'),(220,'admin','2017-02-17 21:23:32','管理员','APPLE1794','BFEBFBFF000306D4'),(221,'admin','2017-02-17 21:37:52','管理员','APPLE1794','BFEBFBFF000306D4'),(222,'admin','2017-02-17 21:39:36','管理员','APPLE1794','BFEBFBFF000306D4'),(223,'admin','2017-02-17 21:40:28','管理员','APPLE1794','BFEBFBFF000306D4'),(224,'admin','2017-02-17 21:45:47','管理员','APPLE1794','BFEBFBFF000306D4'),(225,'admin','2017-02-17 21:48:12','管理员','APPLE1794','BFEBFBFF000306D4'),(226,'admin','2017-02-17 22:24:24','管理员','APPLE1794','BFEBFBFF000306D4'),(227,'admin','2017-02-17 22:40:27','管理员','APPLE1794','BFEBFBFF000306D4'),(228,'admin','2017-02-17 22:42:16','管理员','APPLE1794','BFEBFBFF000306D4'),(229,'admin','2017-02-17 22:44:47','管理员','APPLE1794','BFEBFBFF000306D4'),(230,'admin','2017-02-17 22:50:29','管理员','APPLE1794','BFEBFBFF000306D4'),(231,'admin','2017-02-17 23:09:49','管理员','APPLE1794','BFEBFBFF000306D4'),(232,'admin','2017-02-17 23:22:57','管理员','APPLE1794','BFEBFBFF000306D4'),(233,'admin','2017-02-18 09:55:19','管理员','APPLE1794','BFEBFBFF000306D4'),(234,'admin','2017-02-18 10:13:36','管理员','APPLE1794','BFEBFBFF000306D4'),(235,'admin','2017-02-18 10:17:21','管理员','APPLE1794','BFEBFBFF000306D4'),(236,'admin','2017-02-18 10:26:27','管理员','APPLE1794','BFEBFBFF000306D4'),(237,'admin','2017-02-18 10:28:44','管理员','APPLE1794','BFEBFBFF000306D4'),(238,'admin','2017-02-18 10:50:16','管理员','APPLE1794','BFEBFBFF000306D4'),(239,'admin','2017-02-18 10:59:04','管理员','APPLE1794','BFEBFBFF000306D4'),(240,'admin','2017-02-18 11:03:18','管理员','APPLE1794','BFEBFBFF000306D4'),(241,'admin','2017-02-18 11:06:23','管理员','APPLE1794','BFEBFBFF000306D4'),(242,'admin','2017-02-18 11:09:30','管理员','APPLE1794','BFEBFBFF000306D4'),(243,'admin','2017-02-18 11:10:43','管理员','APPLE1794','BFEBFBFF000306D4'),(244,'admin','2017-02-18 11:46:23','管理员','APPLE1794','BFEBFBFF000306D4'),(245,'admin','2017-02-18 13:49:26','管理员','APPLE1794','BFEBFBFF000306D4'),(246,'admin','2017-02-18 14:10:18','管理员','APPLE1794','BFEBFBFF000306D4'),(247,'admin','2017-02-18 14:17:55','管理员','APPLE1794','BFEBFBFF000306D4'),(248,'admin','2017-02-18 14:26:15','管理员','APPLE1794','BFEBFBFF000306D4'),(249,'admin','2017-02-18 14:27:39','管理员','APPLE1794','BFEBFBFF000306D4'),(250,'admin','2017-02-18 14:34:30','管理员','APPLE1794','BFEBFBFF000306D4'),(251,'admin','2017-02-18 14:44:26','管理员','APPLE1794','BFEBFBFF000306D4'),(252,'admin','2017-02-18 14:53:03','管理员','APPLE1794','BFEBFBFF000306D4'),(253,'admin','2017-02-18 14:54:39','管理员','APPLE1794','BFEBFBFF000306D4'),(254,'admin','2017-02-19 17:12:26','管理员','APPLE1794','BFEBFBFF000306D4'),(255,'admin','2017-02-19 17:13:43','管理员','APPLE1794','BFEBFBFF000306D4'),(256,'admin','2017-02-19 17:42:37','管理员','APPLE1794','BFEBFBFF000306D4'),(257,'admin','2017-02-19 21:34:16','管理员','APPLE1794','BFEBFBFF000306D4'),(258,'admin','2017-02-19 22:27:50','管理员','APPLE1794','BFEBFBFF000306D4'),(259,'admin','2017-02-20 12:21:27','管理员','APPLE1794','BFEBFBFF000306D4'),(260,'admin','2017-02-20 12:24:02','管理员','APPLE1794','BFEBFBFF000306D4'),(261,'admin','2017-02-20 12:26:23','管理员','APPLE1794','BFEBFBFF000306D4'),(262,'admin','2017-02-20 12:27:27','管理员','APPLE1794','BFEBFBFF000306D4'),(263,'admin','2017-02-20 12:31:00','管理员','APPLE1794','BFEBFBFF000306D4'),(264,'admin','2017-02-20 12:36:34','管理员','APPLE1794','BFEBFBFF000306D4'),(265,'admin','2017-02-20 13:39:08','管理员','APPLE1794','BFEBFBFF000306D4'),(266,'admin','2017-02-20 13:45:23','管理员','APPLE1794','BFEBFBFF000306D4'),(267,'admin','2017-02-20 14:02:49','管理员','APPLE1794','BFEBFBFF000306D4'),(268,'admin','2017-02-20 14:08:00','管理员','APPLE1794','BFEBFBFF000306D4'),(269,'admin','2017-02-20 14:12:51','管理员','APPLE1794','BFEBFBFF000306D4'),(270,'admin','2017-02-20 14:21:11','管理员','APPLE1794','BFEBFBFF000306D4'),(271,'admin','2017-02-20 14:42:24','管理员','APPLE1794','BFEBFBFF000306D4'),(272,'admin','2017-02-20 15:05:28','管理员','APPLE1794','BFEBFBFF000306D4'),(273,'admin','2017-02-20 15:12:54','管理员','APPLE1794','BFEBFBFF000306D4'),(274,'admin','2017-02-20 16:18:25','管理员','APPLE1794','BFEBFBFF000306D4'),(275,'admin','2017-02-20 16:24:22','管理员','APPLE1794','BFEBFBFF000306D4'),(276,'admin','2017-02-20 16:32:23','管理员','APPLE1794','BFEBFBFF000306D4'),(277,'admin','2017-02-20 16:36:33','管理员','APPLE1794','BFEBFBFF000306D4'),(278,'admin','2017-02-20 16:39:39','管理员','APPLE1794','BFEBFBFF000306D4'),(279,'admin','2017-02-20 20:54:28','管理员','APPLE1794','BFEBFBFF000306D4'),(280,'admin','2017-02-21 10:40:39','管理员','APPLE1794','BFEBFBFF000306D4'),(281,'admin','2017-02-21 12:10:22','管理员','APPLE1794','BFEBFBFF000306D4'),(282,'admin','2017-02-21 13:40:28','管理员','APPLE1794','BFEBFBFF000306D4'),(283,'admin','2017-02-21 22:56:12','管理员','APPLE1794','BFEBFBFF000306D4'),(284,'admin','2017-02-21 22:58:30','管理员','APPLE1794','BFEBFBFF000306D4'),(285,'admin','2017-02-24 12:51:51','管理员','APPLE1794','BFEBFBFF000306D4'),(286,'admin','2017-02-24 12:54:46','管理员','APPLE1794','BFEBFBFF000306D4'),(287,'admin','2017-02-24 13:03:44','管理员','APPLE1794','BFEBFBFF000306D4'),(288,'admin','2017-02-24 13:05:01','管理员','APPLE1794','BFEBFBFF000306D4'),(289,'admin','2017-02-24 13:08:48','管理员','APPLE1794','BFEBFBFF000306D4');
/*!40000 ALTER TABLE `Log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `NavBarGroups`
--

DROP TABLE IF EXISTS `NavBarGroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `NavBarGroups` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(20) NOT NULL COMMENT '功能组名称',
  `Ico` varchar(100) NOT NULL COMMENT '图标',
  `Sort` int(11) NOT NULL COMMENT '排序',
  `Timestamp` bigint(20) NOT NULL COMMENT '时间戳',
  `Sys` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否是系统级的功能组',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `NavBarGroups`
--

LOCK TABLES `NavBarGroups` WRITE;
/*!40000 ALTER TABLE `NavBarGroups` DISABLE KEYS */;
INSERT INTO `NavBarGroups` (`Id`, `Name`, `Ico`, `Sort`, `Timestamp`, `Sys`) VALUES (1,'办公自动化','images\\Home_32x32.png',0,1487368707,'\0'),(2,'客户管理','images\\Customer_32x32.png',1,1464645441,'\0'),(3,'系统设置','images\\Properties_32x32.png',2,1464645653,'');
/*!40000 ALTER TABLE `NavBarGroups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Permissions`
--

DROP TABLE IF EXISTS `Permissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Permissions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PermissionsName` varchar(50) NOT NULL,
  `Sort` int(11) NOT NULL DEFAULT '99999',
  `Sys` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Permissions`
--

LOCK TABLES `Permissions` WRITE;
/*!40000 ALTER TABLE `Permissions` DISABLE KEYS */;
INSERT INTO `Permissions` (`Id`, `PermissionsName`, `Sort`, `Sys`) VALUES (1,'添加',1,''),(3,'修改',2,''),(4,'删除',3,''),(5,'导出',4,''),(6,'查看',0,''),(7,'打印',5,''),(8,'审核',2147483647,'\0'),(10,'弃审',2147483647,'\0'),(11,'12',2147483647,'\0'),(12,'23',2147483647,'\0'),(14,'45',2147483647,'\0');
/*!40000 ALTER TABLE `Permissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Roles`
--

DROP TABLE IF EXISTS `Roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Roles` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(100) NOT NULL,
  `Timestamp` bigint(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Roles`
--

LOCK TABLES `Roles` WRITE;
/*!40000 ALTER TABLE `Roles` DISABLE KEYS */;
INSERT INTO `Roles` (`Id`, `RoleName`, `Timestamp`) VALUES (1,'管理员',22),(2,'财务部',1),(3,'销售人员',1),(4,'公司主管',1),(5,'飒飒',1484140421);
/*!40000 ALTER TABLE `Roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Staff`
--

DROP TABLE IF EXISTS `Staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Staff` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(50) COLLATE utf8_bin NOT NULL,
  `DeptId` int(11) NOT NULL,
  `Name` varchar(50) COLLATE utf8_bin NOT NULL,
  `Sex` int(1) NOT NULL,
  `Birth` datetime DEFAULT NULL,
  `InTime` datetime NOT NULL,
  `Tel` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `Add` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `Remark` text COLLATE utf8_bin,
  `State` bit(1) NOT NULL,
  `Oper` varchar(50) COLLATE utf8_bin NOT NULL,
  `LogonId` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `LogonName` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `LogonPwd` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `Supper` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Staff`
--

LOCK TABLES `Staff` WRITE;
/*!40000 ALTER TABLE `Staff` DISABLE KEYS */;
INSERT INTO `Staff` (`Id`, `Code`, `DeptId`, `Name`, `Sex`, `Birth`, `InTime`, `Tel`, `Add`, `Remark`, `State`, `Oper`, `LogonId`, `LogonName`, `LogonPwd`, `Supper`) VALUES (14,'YG201207180001',6,'李成',1,'1980-12-12 00:00:00','2008-03-16 00:00:00','13312345678','上海四川路**号','','','系统管理员','admin','admin','123123',''),(22,'YG201207180002',8,'韩萍',0,'1981-07-19 00:00:00','2012-06-05 00:00:00','13912345678','','','\0','',NULL,NULL,NULL,'\0'),(23,'YG201207180003',8,'王晓晓',0,'2012-07-19 00:00:00','2012-07-19 00:00:00','13980897897','','','','',NULL,'27666888888','22','\0'),(24,'YG201607030001',23,'11111',1,'2016-07-05 00:00:00','2016-07-03 00:00:00','111111',NULL,NULL,'','',NULL,'asdf','111','\0'),(25,'YG201607050001',23,'呃呃呃',1,'2016-07-13 00:00:00','2016-07-05 00:00:00','谔谔',NULL,NULL,'','',NULL,NULL,NULL,'\0'),(26,'YG201607050002',8,'323232',1,'2016-07-05 00:00:00','2016-07-05 00:00:00','232323',NULL,NULL,'','',NULL,NULL,NULL,'\0'),(27,'YG201607050003',8,'333',1,'2016-07-05 00:00:00','2016-07-05 00:00:00','323',NULL,NULL,'','',NULL,NULL,NULL,'\0');
/*!40000 ALTER TABLE `Staff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `StaffRoleRelationships`
--

DROP TABLE IF EXISTS `StaffRoleRelationships`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `StaffRoleRelationships` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '员工角色关系表Id',
  `StaffId` int(11) NOT NULL COMMENT '员工编号',
  `RoleId` int(11) NOT NULL COMMENT '角色编号',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `StaffRoleRelationships`
--

LOCK TABLES `StaffRoleRelationships` WRITE;
/*!40000 ALTER TABLE `StaffRoleRelationships` DISABLE KEYS */;
INSERT INTO `StaffRoleRelationships` (`Id`, `StaffId`, `RoleId`) VALUES (1,14,1),(24,24,5);
/*!40000 ALTER TABLE `StaffRoleRelationships` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysSetting`
--

DROP TABLE IF EXISTS `SysSetting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `SysSetting` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ColumnName` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '列名称',
  `Value` varchar(255) COLLATE utf8_bin DEFAULT NULL COMMENT '列对应的值',
  `GroupId` int(11) NOT NULL COMMENT '分组编号',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysSetting`
--

LOCK TABLES `SysSetting` WRITE;
/*!40000 ALTER TABLE `SysSetting` DISABLE KEYS */;
INSERT INTO `SysSetting` (`Id`, `ColumnName`, `Value`, `GroupId`) VALUES (1,'MenuId','Banner 默认',1),(2,'DllPath','SYS\\Frame.SysWindows.dll',1),(3,'EntryFunction','Frame.SysWindows.NetUserControl',1),(4,'Enabled','True',1),(8,'Name','新意信息技术有限公司',2),(9,'RegCode',NULL,2),(10,'Tel',NULL,2),(11,'Fax',NULL,2),(12,'Mail','939381421@qq.com',2),(13,'Bank',NULL,2),(14,'BankCode',NULL,2),(15,'TaxCode',NULL,2),(16,'Copyright','Copyright © 2013-2016',2),(17,'Add',NULL,2),(18,'Remark','',2),(19,'OperMan','admin',2);
/*!40000 ALTER TABLE `SysSetting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TopMenus`
--

DROP TABLE IF EXISTS `TopMenus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `TopMenus` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DisplayName` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '菜单名称',
  `Ico` varchar(100) COLLATE utf8_bin DEFAULT NULL COMMENT '图标',
  `ParentId` int(11) NOT NULL COMMENT '父级编号Id',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `MenuId` varchar(30) COLLATE utf8_bin DEFAULT NULL COMMENT '菜单与 dll 一一对应的窗口编号',
  `DllPath` varchar(100) COLLATE utf8_bin DEFAULT NULL COMMENT 'dll 路径',
  `EntryFunction` varchar(100) COLLATE utf8_bin DEFAULT NULL COMMENT '入口函数',
  `Timestamp` bigint(20) NOT NULL COMMENT '时间戳',
  `Sys` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否是系统级的菜单',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TopMenus`
--

LOCK TABLES `TopMenus` WRITE;
/*!40000 ALTER TABLE `TopMenus` DISABLE KEYS */;
INSERT INTO `TopMenus` (`Id`, `DisplayName`, `Ico`, `ParentId`, `Sort`, `MenuId`, `DllPath`, `EntryFunction`, `Timestamp`, `Sys`) VALUES (1,'文件',NULL,0,1,NULL,NULL,NULL,1486573324,'\0'),(2,'退出',NULL,1,1,'退出','SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControlForTop',1486573324,'\0'),(3,'视图',NULL,0,2,NULL,NULL,NULL,1486573324,'\0'),(4,'复制',NULL,3,1,NULL,NULL,NULL,1486573324,'\0'),(5,'剪切',NULL,3,2,NULL,NULL,NULL,1486573324,'\0'),(6,'粘贴',NULL,3,3,NULL,NULL,NULL,1486573324,'\0'),(7,'帮助',NULL,0,4,NULL,NULL,NULL,1486573324,'\0'),(40,'关于我们',NULL,7,2147483647,'关于我们','SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControlForTop',1486573324,'\0'),(41,'Bug 反馈',NULL,7,2147483647,'Bug 反馈','SYS\\Frame.SysWindows.dll','Frame.SysWindows.NetUserControlForTop',1486573324,'\0'),(42,'服务与支持',NULL,7,2147483647,'','','',1486573324,'\0'),(43,'基础设置',NULL,0,3,NULL,NULL,NULL,1486573324,'\0'),(44,'公司信息',NULL,43,2147483647,'','','',1486573324,'\0'),(45,'客户类型',NULL,43,2147483647,'','','',1486573324,'\0'),(46,'客户来源',NULL,43,2147483647,'','','',1486573324,'\0'),(47,'行业设置',NULL,43,2147483647,'','','',1486573324,'\0'),(48,'2222',NULL,42,2147483647,'','','',1486573324,'\0'),(49,'3333',NULL,48,2147483647,'','','',1486573324,'\0'),(50,'4444',NULL,49,2147483647,'','','',1486573324,'\0');
/*!40000 ALTER TABLE `TopMenus` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-02-24 15:11:43
