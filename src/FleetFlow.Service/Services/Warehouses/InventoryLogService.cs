﻿using AutoMapper;
using FleetFlow.DAL.Repositories;
using FleetFlow.Domain.Configurations;
using FleetFlow.Domain.Congirations;
using FleetFlow.Domain.Entities.Warehouses;
using FleetFlow.Service.DTOs.InventoryLogs;
using FleetFlow.Service.Extentions;
using FleetFlow.Service.Interfaces.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace FleetFlow.Service.Services.Warehouses
{
    public class InventoryLogService : IInventoryLogService
    {
        private readonly Repository<InventoryLog> inventoryRepository;
        private readonly IMapper mapper;

        public InventoryLogService(Repository<InventoryLog> inventoryRepository, IMapper mapper)
        {
            this.inventoryRepository = inventoryRepository;
            this.mapper = mapper;
        }

        public async Task<InventoryLogForResultDto> AddAsync(InventoryLogForCreationDto dto)
        {
            var mapped = this.mapper.Map<InventoryLog>(dto);

            await this.inventoryRepository.InsertAsync(mapped);
            await this.inventoryRepository.SaveAsync();

            return this.mapper.Map<InventoryLogForResultDto>(mapped);
        }

        public async Task<IEnumerable<InventoryLogForResultDto>> RetrieveAllByFiltering(Filter filter, PaginationParams @params = null)
        {
            var logsQuery = this.inventoryRepository.SelectAll(x => x.Type == filter.Type);

            if (filter.OwnerId != null)
                logsQuery = logsQuery.Where(x => x.OwnerId == filter.OwnerId);

            if (filter.ProductId != null)
                logsQuery = logsQuery.Where(x => x.ProductId == filter.ProductId);

            if (@params is null)
            {
                var logs = await logsQuery.ToListAsync();
                return this.mapper.Map<IEnumerable<InventoryLogForResultDto>>(logs);
            }

            var pagedLogs = await logsQuery.ToPagedList(@params).ToListAsync();
            return this.mapper.Map<IEnumerable<InventoryLogForResultDto>>(pagedLogs);
        }




        public async Task<InventoryLogForResultDto> RetrieveById(long id)
        {
            var log = await this.inventoryRepository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
            return this.mapper.Map<InventoryLogForResultDto>(log);
        }
    }
}