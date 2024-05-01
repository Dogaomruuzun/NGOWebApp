--Insert INTO NGOUser
--    (Id, FirstName,LastName,Email, Age, Gender, Password, RegisteredDate)
--Values (1,'Doğa', 'Ömrüuzun', 'doga@ngo.com', 21,0,'','')


--Update NGOUser set RegisteredDate = '2024-05-01 13:57:01' where Id=1

--select * from Role
--select * from NGOUserRoles

--Insert Into NGOUserRoles (NGOUserId, RoleId) VALUES (1,1)

--  select FirstName,LastName,age,Role.RoleName  from NGOUser
--     inner join NGOUserRoles
--     inner join Role
--         on NGOUser.Id = NGOUserRoles.NGOUserId
--         and Role.Id = NGOUserRoles.RoleId


-- Insert INTO NGOUser
--     (Id, FirstName,LastName,Email, Age, Gender, Password, RegisteredDate)
-- Values (2,'Melis', 'Gedik', 'melis@ngo.com', 20,0,'','2024-05-03 10:57:01')



--select * from NGOUserRoles

--insert into NGOUserRoles (NGOUserId, RoleId) VALUES (2,2)

-- Bağış;
-- Kim Yaptı,
-- Ne Bağoışladı
-- Na zaman Bağışladı


-- select NGOUser.FirstName, NGOUser.LastName, Donation.DonationDate,Donation.DonableItemAmount, DonableItem.DonableAmountType  from Donation
--          inner join NGOUser
--              inner join DonableItem
--              on main.NGOUser.Id = Donation.NGOUserId
--              and DonableItem.Id = Donation.DonableItemId
--          where Donation.NGOUserId = 2

--Insert INTO NGOUser
   --(Id, FirstName,LastName,Email, Age, Gender, Password, RegisteredDate)
--Values (5,'Asude','Doğan', 'asude@ngo.com', 19,0, '' ,'2024-05-03 09:57:01')

--select * from NGOUser