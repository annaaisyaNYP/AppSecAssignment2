CREATE TABLE [dbo].[Account] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Email]        NVARCHAR (50)  NOT NULL,
    [FName]        NVARCHAR (50)  NOT NULL,
    [LName]        NVARCHAR (50)  NOT NULL,
    [BirthDate]    DATE           NOT NULL,
    [CCNo]         NVARCHAR (MAX) NOT NULL,
    [CCExMth]      DATE           NOT NULL,
    [CVV]          NVARCHAR (MAX) NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NOT NULL,
    [PasswordSalt] NVARCHAR (MAX) NOT NULL,
    [Key]          NVARCHAR (MAX) NOT NULL,
    [IV]           NVARCHAR (MAX) NOT NULL,
    [PasswordAge]  DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

