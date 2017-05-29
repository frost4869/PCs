using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.DataAccess.Repository
{
    class PcReposity : IBaseRepoAsync<Pc>
    {
        private readonly PCEntities context;

        public PcReposity(PCEntities context)
        {
            this.context = context;
        }

        public async Task ActivateAsync(int id)
        {
            var entity = await this.GetAsync(id);
            entity.Active = true;
            await context.SaveChangesAsync();
        }

        public async Task AddAsync(Pc model)
        {
            context.Pcs.Add(model);
            await context.SaveChangesAsync();
        }

        public async Task DeactivateAsync(int id)
        {
            var entity = await this.GetAsync(id);
            entity.Active = false;
            await this.UpdateAsync(entity);
        }

        public IEnumerable<Pc> GetAllAsync()
        {
            return this.context.Pcs.ToList();
        }

        public async Task<Pc> GetAsync(int id)
        {
            return await this.context.Pcs.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetAsync(id);
            this.context.Pcs.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pc model)
        {
            var entity = await this.GetAsync(model.ID);
            entity.PC_Name = model.PC_Name;
            entity.Type = model.Type;
            entity.HDD = model.HDD;
            entity.CPU = model.CPU;
            entity.RAM = model.RAM;
            entity.OS = model.OS;
            entity.IP = model.IP;
            entity.MAC = model.MAC;
            entity.NV = model.NV;
            entity.PB = model.PB;
            entity.Office_Located = model.Office_Located;

            await this.context.SaveChangesAsync();
        }
    }
}
