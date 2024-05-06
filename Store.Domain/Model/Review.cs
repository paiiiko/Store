﻿using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Model
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime PublicationDate { get; set; }
        [Range(1, 10, ErrorMessage = "Оценка должна быть от {1} до {2}.")]
        public int Grade { get; set; }
        public string? Text { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
    }
}