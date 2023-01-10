using Autofac;
using Security.Application.Queries.Generics;
using Security.Application.Queries.Implementations;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.Mappers;
using Security.Domain.Aggregates.MenuAggregate;
using Security.Domain.Aggregates.ParameterAggregate;
using Security.Domain.Aggregates.ParameterDetailAggregate;
using Security.Domain.Aggregates.ProfileAggregate;
using Security.Domain.Aggregates.ProfileMenuAggregate;
using Security.Domain.Aggregates.SystemsAggregate;
using Security.Domain.Aggregates.UsersAggregate;
using Security.Domain.Aggregates.UsersProfileAggregate;
using Security.Repository.Repositories;

namespace Security.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        private readonly string _defaultConnection;
        private readonly string _key;
        private readonly string _durationInMinute;
        private readonly string _issuer;
        private readonly string _timeZone;
        private readonly string _audience;
        private readonly string _encryptKey;
        private readonly string _encryptIv;

        public ApplicationModule(string defaultConnection, string key, string durationInMinute, string issuer, string timeZone, string audience, string encryptKey, string encryptIv)
        {
            this._defaultConnection = defaultConnection ?? throw new ArgumentNullException(nameof(defaultConnection));
            this._key = key ?? throw new ArgumentNullException(nameof(key));
            this._durationInMinute = durationInMinute ?? throw new ArgumentNullException(nameof(durationInMinute));
            this._issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            this._timeZone = timeZone ?? throw new ArgumentNullException(nameof(timeZone));
            this._audience = audience ?? throw new ArgumentNullException(nameof(audience));
            _encryptKey = encryptKey ?? throw new ArgumentNullException(nameof(encryptKey));
            _encryptIv = encryptIv ?? throw new ArgumentNullException(nameof(encryptIv));
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(r => new GenericQuery(_defaultConnection))
                            .As<IGenericQuery>()
                            .InstancePerLifetimeScope();

            #region ValuesSettings
            builder.Register(r => new ValuesSettings(_timeZone, _encryptKey, _encryptIv))
                .As<IValuesSettings>()
                .InstancePerLifetimeScope();
            #endregion

            #region Mapper

            builder.Register(c => new MenuMapper())
                           .As<IMenuMapper>()
                           .InstancePerLifetimeScope();

            builder.Register(c => new ParameterMapper())
                           .As<IParameterMapper>()
                           .InstancePerLifetimeScope();

            builder.Register(c => new ParameterDetailMapper())
                           .As<IParameterDetailMapper>()
                           .InstancePerLifetimeScope();

            builder.Register(c => new SystemsMapper())
                           .As<ISystemsMapper>()
                           .InstancePerLifetimeScope();

            builder.Register(c => new ProfileMenuMapper())
                           .As<IProfileMenuMapper>()
                           .InstancePerLifetimeScope();

            builder.Register(c => new ProfileMapper())
                           .As<IProfileMapper>()
                           .InstancePerLifetimeScope();

            builder.Register(c => new UsersMapper())
                           .As<IUsersMapper>()
                           .InstancePerLifetimeScope();

            builder.Register(c => new UsersProfileMapper())
                           .As<IUsersProfileMapper>()
                           .InstancePerLifetimeScope();

            builder.Register(c => new UserAuthenticationMapper())
                           .As<IUserAuthenticationMapper>()
                           .InstancePerLifetimeScope();

            #endregion

            #region Query

            builder.RegisterType<MenuQuery>()
                           .As<IMenuQuery>()
                           .InstancePerLifetimeScope();

            builder.RegisterType<ParameterQuery>()
                           .As<IParameterQuery>()
                           .InstancePerLifetimeScope();

            builder.RegisterType<ParameterDetailQuery>()
                           .As<IParameterDetailQuery>()
                           .InstancePerLifetimeScope();

            builder.RegisterType<SystemsQuery>()
                           .As<ISystemsQuery>()
                           .InstancePerLifetimeScope();

            builder.RegisterType<ProfileMenuQuery>()
                           .As<IProfileMenuQuery>()
                           .InstancePerLifetimeScope();

            builder.RegisterType<ProfileQuery>()
                           .As<IProfileQuery>()
                           .InstancePerLifetimeScope();

            builder.RegisterType<UsersQuery>()
                           .As<IUsersQuery>()
                           .InstancePerLifetimeScope();

            builder.RegisterType<UsersProfileQuery>()
                           .As<IUsersProfileQuery>()
                           .InstancePerLifetimeScope();

            builder.Register(r => new TokenQuery(this._key, int.Parse(this._durationInMinute), this._issuer, this._audience))
                .As<ITokenQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserAuthenticationQuery>()
                           .As<IUserAuthenticationQuery>()
                           .InstancePerLifetimeScope();

            #endregion

            #region Repository

            builder.Register(c => new MenuRepository(_defaultConnection))
               .As<IMenuRepository>()
               .InstancePerLifetimeScope();

            builder.Register(c => new ParameterRepository(_defaultConnection))
               .As<IParameterRepository>()
               .InstancePerLifetimeScope();

            builder.Register(c => new ParameterDetailRepository(_defaultConnection))
               .As<IParameterDetailRepository>()
               .InstancePerLifetimeScope();

            builder.Register(c => new SystemsRepository(_defaultConnection))
               .As<ISystemsRepository>()
               .InstancePerLifetimeScope();

            builder.Register(c => new ProfileMenuRepository(_defaultConnection))
               .As<IProfileMenuRepository>()
               .InstancePerLifetimeScope();

            builder.Register(c => new ProfileRepository(_defaultConnection))
               .As<IProfileRepository>()
               .InstancePerLifetimeScope();

            builder.Register(c => new UsersRepository(_defaultConnection))
               .As<IUsersRepository>()
               .InstancePerLifetimeScope();

            builder.Register(c => new UsersProfileRepository(_defaultConnection))
               .As<IUsersProfileRepository>()
               .InstancePerLifetimeScope();

            #endregion

        }
    }
}
