CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id int AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: User Account',
  name VARCHAR (255) NOT NULL COMMENT 'keep name',
  description VARCHAR (255) DEFAULT '',
  img VARCHAR (255) NOT NULL COMMENT 'keep image',
  views INT NOT NULL,
  keeps INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
ALTER TABLE
  keeps
ADD
  COLUMN keeps INT NOT NULL;
CREATE TABLE IF NOT EXISTS vaults(
    id int AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
    creatorId VARCHAR(255) NOT NULL COMMENT 'FK: User Account',
    name VARCHAR (255) NOT NULL COMMENT 'vault name',
    description VARCHAR (255) DEFAULT '',
    isPrivate BOOLEAN NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
  ) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vault_keeps(
    id int AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
    creatorId VARCHAR(255) NOT NULL COMMENT 'FK: User Account',
    vaultId INT NOT NULL COMMENT 'FK',
    keepID INT NOT NULL COMMENT 'FK',
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE
  ) default charset utf8 COMMENT '';
-- SELECT
  --   k.*,
  --   v.name as vaultName,
  --   v.description as vaultDescription,
  --   v.isPrivate,
  --   vk.id as vaultKeepId,
  --   vk.keepId as keepId,
  --   vk.vaultId as vaultId
  -- FROM
  --   vault_keeps vk
  --   JOIN vaults v ON v.id = vk.vaultId
  --   JOIN keeps k ON k.id = vk.keepId;