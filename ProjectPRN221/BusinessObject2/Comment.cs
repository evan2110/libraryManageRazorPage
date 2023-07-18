using System;
using System.Collections.Generic;

namespace ProjectPRN221.BusinessObject2;

public partial class Comment
{
    public int CommentId { get; set; }

    public int BookId { get; set; }

    public string Content { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
