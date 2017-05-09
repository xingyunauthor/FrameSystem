/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 50712
 Source Host           : localhost
 Source Database       : FRAME_MYSQL

 Target Server Type    : MySQL
 Target Server Version : 50712
 File Encoding         : utf-8

 Date: 05/09/2017 21:22:17 PM
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
--  Table structure for `Dept`
-- ----------------------------
DROP TABLE IF EXISTS `Dept`;
CREATE TABLE `Dept` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
--  Records of `Dept`
-- ----------------------------
BEGIN;
INSERT INTO `Dept` VALUES ('1', '0', '部门'), ('6', '1', '总经办'), ('7', '1', '市场部'), ('8', '1', '财务部'), ('9', '1', '采购部'), ('10', '6', '总经理'), ('11', '0', '43434343'), ('12', '1', '额威威'), ('13', '12', '55555'), ('14', '13', '55555555'), ('15', '11', '5444'), ('16', '15', '32323'), ('17', '6', '333333'), ('18', '17', '4444444444'), ('19', '18', '1'), ('20', '7', '2222'), ('21', '20', '33'), ('22', '18', '3333'), ('23', '0', '谔谔谔谔');
COMMIT;

-- ----------------------------
--  Table structure for `LeftMenuPermissions`
-- ----------------------------
DROP TABLE IF EXISTS `LeftMenuPermissions`;
CREATE TABLE `LeftMenuPermissions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LeftMenuId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  `PermissionsId` int(11) NOT NULL,
  `Have` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=156 DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `LeftMenuPermissions`
-- ----------------------------
BEGIN;
INSERT INTO `LeftMenuPermissions` VALUES ('1', '6', '4', '6', '1'), ('2', '4', '4', '1', '1'), ('3', '7', '4', '6', '1'), ('4', '5', '4', '4', '1'), ('5', '1', '1', '6', '0'), ('6', '2', '1', '6', '0'), ('7', '1', '1', '1', '0'), ('8', '1', '1', '3', '0'), ('9', '1', '1', '4', '0'), ('10', '2', '2', '6', '1'), ('11', '1', '1', '5', '0'), ('12', '1', '1', '7', '1'), ('13', '5', '1', '4', '1'), ('14', '4', '1', '3', '1'), ('15', '3', '1', '1', '0'), ('16', '6', '1', '5', '0'), ('17', '7', '1', '7', '1'), ('18', '7', '1', '6', '0'), ('19', '8', '1', '1', '0'), ('20', '9', '1', '3', '0'), ('21', '10', '1', '4', '0'), ('22', '11', '1', '5', '1'), ('23', '12', '1', '7', '0'), ('24', '45', '1', '5', '0'), ('25', '45', '1', '6', '1'), ('26', '45', '1', '1', '1'), ('27', '45', '1', '3', '1'), ('28', '45', '1', '4', '1'), ('30', '3', '1', '6', '0'), ('31', '4', '1', '6', '0'), ('32', '5', '1', '6', '0'), ('33', '6', '1', '6', '0'), ('34', '8', '1', '6', '0'), ('35', '9', '1', '6', '0'), ('36', '10', '1', '6', '0'), ('37', '2', '1', '3', '0'), ('38', '3', '1', '4', '0'), ('39', '4', '1', '5', '1'), ('40', '5', '1', '7', '1'), ('41', '4', '1', '4', '1'), ('42', '4', '1', '1', '1'), ('44', '21', '3', '4', '1'), ('51', '1', '1', '24', '1'), ('52', '2', '1', '24', '1'), ('53', '3', '1', '24', '1'), ('54', '4', '1', '24', '1'), ('55', '5', '1', '24', '1'), ('56', '6', '1', '24', '1'), ('57', '7', '1', '24', '1'), ('58', '9', '1', '24', '1'), ('59', '8', '1', '24', '1'), ('60', '10', '1', '24', '1'), ('61', '11', '1', '24', '1'), ('62', '12', '1', '24', '1'), ('63', '13', '1', '24', '1'), ('64', '14', '1', '24', '1'), ('65', '15', '1', '24', '1'), ('66', '16', '1', '24', '1'), ('67', '17', '1', '24', '1'), ('68', '18', '1', '24', '1'), ('71', '2', '1', '7', '1'), ('72', '3', '1', '7', '1'), ('73', '4', '1', '7', '1'), ('74', '6', '1', '7', '1'), ('75', '8', '1', '7', '1'), ('76', '9', '1', '7', '1'), ('77', '10', '1', '7', '1'), ('78', '11', '1', '6', '0'), ('79', '12', '1', '6', '0'), ('80', '13', '1', '6', '0'), ('81', '14', '1', '6', '0'), ('82', '15', '1', '6', '0'), ('83', '16', '1', '6', '0'), ('84', '17', '1', '6', '0'), ('85', '19', '1', '6', '0'), ('86', '18', '1', '6', '0'), ('87', '20', '1', '6', '0'), ('88', '21', '1', '6', '0'), ('89', '22', '1', '6', '0'), ('90', '23', '1', '6', '0'), ('91', '24', '1', '6', '0'), ('92', '25', '1', '6', '0'), ('93', '27', '1', '6', '0'), ('94', '26', '1', '6', '0'), ('95', '45', '2', '6', '1'), ('96', '46', '2', '6', '1'), ('97', '36', '2', '6', '1'), ('98', '46', '1', '6', '1'), ('99', '36', '1', '6', '1'), ('100', '34', '1', '6', '1'), ('101', '33', '1', '6', '1'), ('102', '7', '5', '6', '0'), ('103', '7', '5', '1', '0'), ('104', '28', '1', '6', '0'), ('105', '29', '1', '6', '0'), ('106', '30', '1', '6', '0'), ('107', '31', '1', '6', '0'), ('108', '32', '1', '6', '0'), ('109', '35', '1', '6', '1'), ('110', '37', '1', '6', '1'), ('111', '38', '1', '6', '1'), ('112', '44', '1', '6', '0'), ('113', '47', '1', '6', '1'), ('114', '48', '1', '6', '1'), ('115', '49', '1', '6', '1'), ('116', '50', '1', '6', '1'), ('117', '51', '1', '6', '1'), ('118', '52', '1', '6', '0'), ('119', '2', '1', '10', '1'), ('120', '9', '1', '1', '1'), ('121', '53', '1', '6', '1'), ('122', '54', '1', '6', '1'), ('123', '55', '1', '6', '1'), ('124', '56', '1', '6', '1'), ('125', '57', '1', '6', '1'), ('126', '34', '1', '1', '0'), ('127', '34', '1', '3', '1'), ('128', '34', '1', '4', '1'), ('129', '34', '1', '5', '1'), ('130', '34', '1', '7', '0'), ('131', '53', '1', '1', '0'), ('132', '53', '1', '3', '0'), ('133', '53', '1', '4', '0'), ('134', '36', '1', '1', '0'), ('135', '36', '1', '3', '0'), ('136', '36', '1', '4', '0'), ('137', '36', '1', '5', '0'), ('138', '36', '1', '7', '1'), ('139', '33', '1', '1', '1'), ('140', '33', '1', '3', '1'), ('141', '33', '1', '4', '0'), ('142', '37', '1', '3', '1'), ('143', '54', '1', '1', '1'), ('144', '54', '1', '3', '1'), ('145', '54', '1', '4', '1'), ('146', '60', '1', '6', '0'), ('147', '63', '1', '6', '0'), ('148', '5', '1', '3', '1'), ('149', '64', '1', '6', '0'), ('150', '56', '1', '4', '1'), ('151', '56', '1', '1', '1'), ('152', '65', '1', '6', '1'), ('153', '65', '1', '3', '1'), ('154', '56', '1', '3', '1'), ('155', '37', '1', '7', '0');
COMMIT;

-- ----------------------------
--  Table structure for `LeftMenus`
-- ----------------------------
DROP TABLE IF EXISTS `LeftMenus`;
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

-- ----------------------------
--  Records of `LeftMenus`
-- ----------------------------
BEGIN;
INSERT INTO `LeftMenus` VALUES ('1', '内部公告', null, '0', '4', '呃呃呃', '1', 'ClassLibrary1.dll', 'ClassLibrary1.Class1', b'0', '1487086443', b'0'), ('2', '00000', null, '0', '1', '1', '1', '11', '11', b'0', '1487086443', b'0'), ('3', '我发送的短信', null, '2', '1', null, '1', null, null, b'0', '1487086443', b'0'), ('4', '短信群发', null, '2', '999999', null, '1', null, null, b'0', '1487086443', b'0'), ('5', '短信发送记录', null, '2', '2', null, '1', null, null, b'0', '1487086443', b'0'), ('6', '事物管理', null, '0', '5', null, '1', null, null, b'0', '1487086443', b'0'), ('7', '待办事物', null, '6', '0', null, '1', null, null, b'0', '1487086443', b'0'), ('8', '已办事物', null, '6', '0', null, '1', null, null, b'0', '1487086443', b'0'), ('9', '下属事物', null, '6', '0', null, '1', null, null, b'0', '1487086443', b'0'), ('10', '审批管理', null, '0', '3', null, '1', null, null, b'0', '1487086443', b'0'), ('11', '我的申请', null, '0', '7', null, '1', null, null, b'0', '1487086443', b'0'), ('12', '经我审批', null, '10', '0', null, '1', null, null, b'0', '1487086443', b'0'), ('13', '文档管理', null, '0', '6', null, '1', null, null, b'0', '1487086443', b'0'), ('14', '私有文档', null, '13', '0', null, '1', null, null, b'0', '1487086443', b'0'), ('15', '公有文档', null, '13', '0', null, '1', null, null, b'0', '1487086443', b'0'), ('16', '任务管理', null, '0', '8', null, '1', null, null, b'0', '1487086443', b'0'), ('17', '我创建的', null, '16', '0', null, '1', null, null, b'0', '1487086443', b'0'), ('18', '分配给我的', null, '16', '0', null, '1', null, null, b'0', '1487086443', b'0'), ('19', '客户资料', null, '0', '9', null, '1', null, null, b'0', '1487086443', b'0'), ('20', '联系人', null, '0', '2', '呃呃呃得到', '1', 'C:\\PersonalProject\\FrameSystem\\Frame.Utils\\bin\\Debug\\Frame.Utils.dll', 'Frame.Utils.Functions', b'0', '1487086443', b'0'), ('21', '职员档案管理', null, '0', '10', null, '1', null, null, b'0', '1487086443', b'0'), ('22', '收支管理', null, '0', '11', null, '1', null, null, b'0', '1487086443', b'0'), ('23', '新增客户', null, '0', '1', null, '2', null, null, b'0', '1487155563', b'0'), ('24', '客户资料', null, '0', '4', null, '2', null, null, b'0', '1487155563', b'0'), ('25', '联系人', null, '0', '2', null, '2', null, null, b'0', '1487155563', b'0'), ('26', '客户跟进', null, '0', '3', null, '2', null, null, b'0', '1487155563', b'0'), ('27', '跟进提醒', null, '26', '0', null, '2', null, null, b'0', '1487155563', b'0'), ('28', '客户合同', null, '0', '5', null, '2', null, null, b'0', '1487155563', b'0'), ('29', '合同到期提醒', null, '0', '6', null, '2', null, null, b'0', '1487155563', b'0'), ('30', '客户分布', null, '0', '7', null, '2', null, null, b'0', '1487155563', b'0'), ('31', '客户结算', null, '0', '8', null, '2', null, null, b'0', '1487155563', b'0'), ('32', '系统配置', null, '0', '2', null, '3', null, null, b'0', '1487350598', b'0'), ('33', '操作员管理', null, '0', '3', '操作员管理', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487350598', b'0'), ('34', '员工档案', null, '0', '4', '用户管理', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487350598', b'0'), ('36', '登陆日志管理', null, '0', '6', '登陆日志管理', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487350598', b'0'), ('37', '公司信息', null, '0', '7', '公司信息设置', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487350598', b'0'), ('44', '顶顶顶', null, '20', '99999', '权限管理000', '1', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487086443', b'0'), ('45', '权限管理', null, '0', '8', '权限管理', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487350598', b'0'), ('51', '修改密码', null, '0', '9', '修改密码', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487350598', b'0'), ('52', '系统初始化', null, '0', '10', '系统初始化', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487350598', b'0'), ('53', '部门管理', null, '0', '11', '部门管理', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'1', '1487350598', b'0'), ('54', '左侧菜单管理', null, '0', '12', '左侧菜单管理', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'1', '1487350598', b'1'), ('55', '顶部菜单管理', null, '0', '13', '顶部菜单管理', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487350598', b'0'), ('56', '功能组管理', null, '0', '14', '功能组管理', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487350598', b'0'), ('60', '嗡嗡嗡', null, '23', '15', '', '2', '', '', b'0', '1487155563', b'0'), ('63', '6666', null, '60', '16', '', '2', '', '', b'0', '1487155563', b'0'), ('64', '登陆主题设置', null, '0', '15', '登陆主题设置', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487367556', b'0'), ('65', 'Banner 设置', null, '0', '16', 'Banner 设置', '3', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControl', b'0', '1487524385', b'0');
COMMIT;

-- ----------------------------
--  Table structure for `Log`
-- ----------------------------
DROP TABLE IF EXISTS `Log`;
CREATE TABLE `Log` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LoginName` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `LoginTime` datetime DEFAULT NULL,
  `LoginRole` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `LoginMach` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `LoginCpu` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=292 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
--  Table structure for `NavBarGroups`
-- ----------------------------
DROP TABLE IF EXISTS `NavBarGroups`;
CREATE TABLE `NavBarGroups` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(20) NOT NULL COMMENT '功能组名称',
  `Ico` varchar(100) NOT NULL COMMENT '图标',
  `Sort` int(11) NOT NULL COMMENT '排序',
  `Timestamp` bigint(20) NOT NULL COMMENT '时间戳',
  `Sys` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否是系统级的功能组',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `NavBarGroups`
-- ----------------------------
BEGIN;
INSERT INTO `NavBarGroups` VALUES ('1', '办公自动化', 'images\\Home_32x32.png', '0', '1487368707', b'0'), ('2', '客户管理', 'images\\Customer_32x32.png', '1', '1464645441', b'0'), ('3', '系统设置', 'images\\Properties_32x32.png', '2', '1464645653', b'1');
COMMIT;

-- ----------------------------
--  Table structure for `Permissions`
-- ----------------------------
DROP TABLE IF EXISTS `Permissions`;
CREATE TABLE `Permissions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PermissionsName` varchar(50) NOT NULL,
  `Sort` int(11) NOT NULL DEFAULT '99999',
  `Sys` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `Permissions`
-- ----------------------------
BEGIN;
INSERT INTO `Permissions` VALUES ('1', '添加', '1', b'1'), ('3', '修改', '2', b'1'), ('4', '删除', '3', b'1'), ('5', '导出', '4', b'1'), ('6', '查看', '0', b'1'), ('7', '打印', '5', b'1'), ('8', '审核', '2147483647', b'0'), ('10', '弃审', '2147483647', b'0'), ('11', '12', '2147483647', b'0'), ('12', '23', '2147483647', b'0'), ('14', '45', '2147483647', b'0');
COMMIT;

-- ----------------------------
--  Table structure for `Roles`
-- ----------------------------
DROP TABLE IF EXISTS `Roles`;
CREATE TABLE `Roles` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(100) NOT NULL,
  `Timestamp` bigint(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `Roles`
-- ----------------------------
BEGIN;
INSERT INTO `Roles` VALUES ('1', '管理员', '22'), ('2', '财务部', '1'), ('3', '销售人员', '1'), ('4', '公司主管', '1'), ('5', '飒飒', '1484140421');
COMMIT;

-- ----------------------------
--  Table structure for `Staff`
-- ----------------------------
DROP TABLE IF EXISTS `Staff`;
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

-- ----------------------------
--  Records of `Staff`
-- ----------------------------
BEGIN;
INSERT INTO `Staff` VALUES ('14', 'YG201207180001', '6', '李成', '1', '1980-12-12 00:00:00', '2008-03-16 00:00:00', '13312345678', '上海四川路**号', '', b'1', '系统管理员', 'admin', 'admin', '123123', b'1'), ('22', 'YG201207180002', '8', '韩萍', '0', '1981-07-19 00:00:00', '2012-06-05 00:00:00', '13912345678', '', '', b'0', '', null, null, null, b'0'), ('23', 'YG201207180003', '8', '王晓晓', '0', '2012-07-19 00:00:00', '2012-07-19 00:00:00', '13980897897', '', '', b'1', '', null, '27666888888', '22', b'0'), ('24', 'YG201607030001', '23', '11111', '1', '2016-07-05 00:00:00', '2016-07-03 00:00:00', '111111', null, null, b'1', '', null, 'asdf', '111', b'0'), ('25', 'YG201607050001', '23', '呃呃呃', '1', '2016-07-13 00:00:00', '2016-07-05 00:00:00', '谔谔', null, null, b'1', '', null, null, null, b'0'), ('26', 'YG201607050002', '8', '323232', '1', '2016-07-05 00:00:00', '2016-07-05 00:00:00', '232323', null, null, b'1', '', null, null, null, b'0'), ('27', 'YG201607050003', '8', '333', '1', '2016-07-05 00:00:00', '2016-07-05 00:00:00', '323', null, null, b'1', '', null, null, null, b'0');
COMMIT;

-- ----------------------------
--  Table structure for `StaffRoleRelationships`
-- ----------------------------
DROP TABLE IF EXISTS `StaffRoleRelationships`;
CREATE TABLE `StaffRoleRelationships` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '员工角色关系表Id',
  `StaffId` int(11) NOT NULL COMMENT '员工编号',
  `RoleId` int(11) NOT NULL COMMENT '角色编号',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `StaffRoleRelationships`
-- ----------------------------
BEGIN;
INSERT INTO `StaffRoleRelationships` VALUES ('1', '14', '1'), ('24', '24', '5');
COMMIT;

-- ----------------------------
--  Table structure for `SysSetting`
-- ----------------------------
DROP TABLE IF EXISTS `SysSetting`;
CREATE TABLE `SysSetting` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ColumnName` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '列名称',
  `Value` varchar(255) COLLATE utf8_bin DEFAULT NULL COMMENT '列对应的值',
  `GroupId` int(11) NOT NULL COMMENT '分组编号',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
--  Records of `SysSetting`
-- ----------------------------
BEGIN;
INSERT INTO `SysSetting` VALUES ('1', 'MenuId', 'Banner 默认', '1'), ('2', 'DllPath', 'SYS\\Frame.SysWindows.dll', '1'), ('3', 'EntryFunction', 'Frame.SysWindows.NetUserControl', '1'), ('4', 'Enabled', 'True', '1'), ('8', 'Name', '新意工作室', '2'), ('9', 'RegCode', null, '2'), ('10', 'Tel', '13291208896', '2'), ('11', 'Fax', null, '2'), ('12', 'Mail', 'devapplication@foxmail.com', '2'), ('13', 'Bank', null, '2'), ('14', 'BankCode', null, '2'), ('15', 'TaxCode', null, '2'), ('16', 'Copyright', 'Copyright © 2013-2016', '2'), ('17', 'Add', null, '2'), ('18', 'Remark', '官方网址：http://www.devapplication.com', '2'), ('19', 'OperMan', 'admin', '2');
COMMIT;

-- ----------------------------
--  Table structure for `TopMenus`
-- ----------------------------
DROP TABLE IF EXISTS `TopMenus`;
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

-- ----------------------------
--  Records of `TopMenus`
-- ----------------------------
BEGIN;
INSERT INTO `TopMenus` VALUES ('1', '文件', null, '0', '1', null, null, null, '1486573324', b'0'), ('2', '退出', null, '1', '1', '退出', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControlForTop', '1486573324', b'0'), ('3', '视图', null, '0', '2', null, null, null, '1486573324', b'0'), ('4', '复制', null, '3', '1', null, null, null, '1486573324', b'0'), ('5', '剪切', null, '3', '2', null, null, null, '1486573324', b'0'), ('6', '粘贴', null, '3', '3', null, null, null, '1486573324', b'0'), ('7', '帮助', null, '0', '4', null, null, null, '1486573324', b'0'), ('40', '关于我们', null, '7', '2147483647', '关于我们', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControlForTop', '1486573324', b'0'), ('41', 'Bug 反馈', null, '7', '2147483647', 'Bug 反馈', 'SYS\\Frame.SysWindows.dll', 'Frame.SysWindows.NetUserControlForTop', '1486573324', b'0'), ('42', '服务与支持', null, '7', '2147483647', '', '', '', '1486573324', b'0'), ('43', '基础设置', null, '0', '3', null, null, null, '1486573324', b'0'), ('44', '公司信息', null, '43', '2147483647', '', '', '', '1486573324', b'0'), ('45', '客户类型', null, '43', '2147483647', '', '', '', '1486573324', b'0'), ('46', '客户来源', null, '43', '2147483647', '', '', '', '1486573324', b'0'), ('47', '行业设置', null, '43', '2147483647', '', '', '', '1486573324', b'0'), ('48', '2222', null, '42', '2147483647', '', '', '', '1486573324', b'0'), ('49', '3333', null, '48', '2147483647', '', '', '', '1486573324', b'0'), ('50', '4444', null, '49', '2147483647', '', '', '', '1486573324', b'0');
COMMIT;

SET FOREIGN_KEY_CHECKS = 1;
