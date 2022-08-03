USE [ShoeCollection]
GO

set identity_insert [Brand] on
insert into [Brand] ([Id], BrandName) Values (1, 'Nike'), (2, 'Adidas'), (3, 'Jordan');
set identity_insert [Brand] off

set identity_insert [Style] on
insert into [Style] ([Id], [Name]) Values(1, 'AirMax'), (2, 'UltraBoost 4 DNA'), (3, 'Air Jordan Retro 1');
set identity_insert [Style] off

set identity_insert [User] on
insert into [UserProfile] ([Id], FirstName, LastName, FirebaseUserId, Email )
Values(1, 'Sam', 'Smith', 'lnYKDUw9p5NH92ZqmTFWur2CcIH2', 'sam@no.com')
insert into [UserProfile] ([Id], FirstName, LastName, FirebaseUserId, Email )
Values(2, 'Bob', 'Hope', 'oLRY99D5upaBSuH0PssKwazvUHY2', 'bob@no.com')
insert into [UserProfile] ([Id], FirstName, LastName, FirebaseUserId, Email )
Values(3, 'Tim', 'Jones', '2bPrek1P5cVzq2rJa8c1emcKFwu1', 'timjones@no.com')
set identity_insert [User] off



set identity_insert [Shoe] on
insert into [Shoe] ([Id], UserId, Size, StyleId, BrandId, ImageUrl)
Values (1, 1, 10, 1, 1, 'https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/bbbwgncnxexhwgbz8qbp/air-max-2017-mens-shoes-BVqnkV.png')
insert into [Shoe] ([Id], UserId, Size, StyleId, BrandId, ImageUrl)
Values (2, 2, 11, 2, 2, 'https://assets.adidas.com/images/h_840,f_auto,q_auto,fl_lossy,c_fill,g_auto/dbed4f9ff42c4bcdacccae6001369da0_9366/Ultraboost_22_Shoes_White_HP2485_01_standard.jpg')
insert into [Shoe] ([Id], UserId, Size, StyleId, BrandId, ImageUrl)
Values (3, 3, 14, 3, 3, 'https://cdn.flightclub.com/1800/TEMPLATE/299069/1.jpg')
set identity_insert [Shoe] off



set identity_insert [Favorite] on
insert into [Favorite] ([Id], UserId, ShoeId)
Values (1, 1, 3)
set identity_insert [Favorite] off



