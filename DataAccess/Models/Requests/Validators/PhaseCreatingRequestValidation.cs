﻿using FluentValidation;

namespace DataAccess.Models.Requests.Validators
{
    public class PhaseCreatingRequestValidation : AbstractValidator<PhaseRequest>
    {
        public PhaseCreatingRequestValidation()
        {
            RuleFor(obj => obj.EstimatedStartDate)
                .NotEmpty()
                .WithMessage("Ngày bắt đầu không được để trống.")
                .Must(BeAValidDate)
                .WithMessage("Ngày bắt đầu không hợp lệ.")
                .Must(BeNotInPast)
                .WithMessage("Ngày bắt đầu không được trong quá khứ.");
            RuleFor(obj => obj.EstimatedEndDate)
                .NotEmpty()
                .WithMessage("Ngày kết thúc không được để trống.")
                .Must(BeAValidDate)
                .WithMessage("Ngày kết thúc không hợp lệ.")
                .Must(BeNotInPast)
                .WithMessage("Ngày kết thúc không được trong quá khứ.")
                .Must((model, e) => e >= model.EstimatedStartDate)
                .WithMessage("Ngày kết thúc phải sau ngày bắt đầu.");
            RuleFor(obj => obj.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100)
                .MinimumLength(5)
                .WithMessage("Tên phải từ 5 đến 100 kí tự");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date >= DateTime.MinValue && date <= DateTime.MaxValue;
        }

        private bool BeNotInPast(DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }
    }
}
