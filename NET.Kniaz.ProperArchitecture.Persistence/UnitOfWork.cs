using Microsoft.EntityFrameworkCore;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        DataBaseContext _context;
        private CurrencyRepository _currencyRepository;
        private ResourceRepository _resourceRepository;
        private ProjectRepository _projectRepository;
        private TeamRepository _teamRepository;
        private TeamMemberRepository _teamMemberRepository;
        private StoryRepository _storyRepository;
        private SprintRepository _sprintRepository;
        private CommitRepository _commitRepository;
        private SimulationResultRepository _simulationResultRepository;
        

        private bool disposed = false;
        
        public UnitOfWork(DataBaseContext context) 
        { 
            _context = context;

            if (this._currencyRepository == null)
            {
                this._currencyRepository = new CurrencyRepository(_context);
            }

            if (this._resourceRepository == null)
            {
                this._resourceRepository = new ResourceRepository(_context);
            }

            if (this._projectRepository == null)
            {
                this._projectRepository = new ProjectRepository(_context);
            }

            if(this._teamRepository == null)
            {
                this._teamRepository = new TeamRepository(_context);
            }

            if (this._teamMemberRepository == null)
            {
                this._teamMemberRepository = new TeamMemberRepository(_context);
            }

            if (_storyRepository == null)
            {
                this._storyRepository = new StoryRepository(_context);  
            }

            if (_sprintRepository == null)
            {
                this._sprintRepository = new SprintRepository(_context);    
            }

            if (_commitRepository == null)
            {
                this._commitRepository = new CommitRepository(_context);   
            }

            if (_simulationResultRepository == null)
            {
                this._simulationResultRepository = new SimulationResultRepository(_context);
            }
        }

        public IEntityRepository<Currency> CurrencyRepository
        {
            get
            {

                return this._currencyRepository;
            }
        }

        public IEntityRepository<Resource> ResourceRepository
        {
            get
            {

                return this._resourceRepository;
            }
        }

        public IEntityRepository<Project> ProjectRepository
        {
            get
            {
                return this._projectRepository;
            }
        }

        public IEntityRepository<Team> TeamRepository
        {
            get
            {
                return this._teamRepository;
            }
        }

        public IEntityRepository<TeamMember> TeamMemberRepository
        {
            get
            {
                return this._teamMemberRepository;
            }
        }

        public IEntityRepository<Sprint> SprintRepository
        {
            get
            { return this._sprintRepository;}
        }

        public IEntityRepository<Story> StoryRepository
        {
            get
            { return this._storyRepository; }
        }

        public IEntityRepository<Commit> CommitRepository
        {
            get
            { return this._commitRepository; }
        }

        public IEntityRepository<SimulationResult> SimulationResultRepository
        {
            get
            { return this._simulationResultRepository; }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DetachAllEntities()
        {
            // Get all the entities that are being tracked by the DbContext
            var trackedEntities = _context.ChangeTracker.Entries().ToList();

            // Loop through all the tracked entities and set their state to Detached
            foreach (var entry in trackedEntities)
            {
                if (entry.Entity != null)
                {
                    entry.State = EntityState.Detached;
                }
            }
        }

    }
}
