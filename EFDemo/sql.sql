CREATE table Student
(ID int identity(1,1) primary key,Name nvarchar(50),ClassLevel int,Age int,BirthDay date)

create table Teacher
(ID int identity(1,1) primary key,Name nvarchar(50))

create table Class
(ID int identity(1,1) primary key,Name nvarchar(50),TeacherID int foreign key references Teacher(ID))

create table SudentClass
(ID int identity(1,1) primary key,TeacherID int foreign key references Teacher(ID),SudentID int foreign key references Student(ID))


