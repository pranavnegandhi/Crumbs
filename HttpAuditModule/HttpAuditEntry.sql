CREATE TABLE [audit].[HttpAuditEntry]
([HttpAuditEntryId]   [INT] IDENTITY(1, 1)
                            CONSTRAINT [PK_HttpAuditEntry] PRIMARY KEY CLUSTERED ([HttpAuditEntryId] ASC), 
 [TimeStamp]          [DATETIME] NOT NULL
                                 CONSTRAINT [DF_HttpAuditEntry_CreatedOn] DEFAULT(GETDATE()), 
 [RawUrl]             [NVARCHAR](255) NOT NULL, 
 [Path]               [NVARCHAR](255) NOT NULL, 
 [HttpMethod]         [NVARCHAR](255) NOT NULL, 
 [HttpStatus]         [INT] NOT NULL, 
 [UserName]           [NVARCHAR](255) NOT NULL, 
 [AuthenticationType] [NVARCHAR](255) NOT NULL, 
 [ContentType]        [NVARCHAR](255) NOT NULL, 
 [UserAgent]          [NVARCHAR](255) NOT NULL, 
 [HostAddress]        [NVARCHAR](255) NOT NULL, 
 [HostName]           [NVARCHAR](255) NOT NULL, 
 [Referrer]           [NVARCHAR](255) NOT NULL
);
GO