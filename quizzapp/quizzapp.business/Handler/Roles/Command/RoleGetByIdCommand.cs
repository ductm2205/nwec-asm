using System;
using quizzapp.business.Handler.Base;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Roles.Command
{
    public class RoleGetByIdCommand : BaseCommand<Role>
    {
        public Guid Id { get; set; }
    }
}