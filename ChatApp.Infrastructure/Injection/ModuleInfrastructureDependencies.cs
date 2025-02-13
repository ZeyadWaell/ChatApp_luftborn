using ChatApp.Core.Interfaces.Main;
using ChatApp.Infrastructure.Repositories.Main;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Api.ModuleInfrastructureDependencies
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
