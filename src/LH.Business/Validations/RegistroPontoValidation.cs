using FluentValidation;
using LH.Business.Models;
using LH.Business.Validations.Documentos;

namespace LH.Business.Validations
{
    public class RegistroPontoValidation : AbstractValidator<RegistroPonto>
    {
        public RegistroPontoValidation()
        {
            //RuleFor(f => f.Codigo)
            //    .NotEmpty()
            //    .WithMessage("O campo {PropertyName} precisa ser fornecido!");
            RuleFor(f => f.Nome)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido!");
            RuleFor(f => f.ValorHora)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido!");
            //RuleFor(f => f.Data)
            //    .NotEmpty()
            //    .WithMessage("O campo {PropertyName} precisa ser fornecido!");

            //RuleFor(f => f.Entrada)
            //   .NotEmpty()
            //   .WithMessage("O campo {PropertyName} precisa ser fornecido!");

            //RuleFor(f => f.Saida)
            //   .NotEmpty()
            //   .WithMessage("O campo {PropertyName} precisa ser fornecido!");

            //RuleFor(f => f.Almoco)
            //   .NotEmpty()
            //   .WithMessage("O campo {PropertyName} precisa ser fornecido!");
        }
    }
}
