CREATE VIEW [dbo].[vw_PostView]
AS SELECT
    p.Id AS PostId,
    p.Title,
    p.Content,
    p.CreatedOn,
    u.Name AS Author,
    ISNULL(l.TotalLikes, 0) AS TotalLikes,
    c.Id AS CommentId,
    c.Content AS CommentContent,
    c.CreatedOn AS CommentCreatedOn,
    cu.Name AS CommentAuthor,
    t.Name AS TagName
FROM tb_Post p
INNER JOIN tb_User u ON u.Id = p.UserId
LEFT JOIN (
    SELECT PostId, COUNT(*) AS TotalLikes
    FROM tb_Like
    GROUP BY PostId
) l ON l.PostId = p.Id
LEFT JOIN tb_Comment c ON c.PostId = p.Id
LEFT JOIN tb_User cu ON cu.Id = c.UserId
LEFT JOIN tb_PostTag pt ON pt.PostId = p.Id
LEFT JOIN tb_Tag t ON t.Id = pt.TagId;
