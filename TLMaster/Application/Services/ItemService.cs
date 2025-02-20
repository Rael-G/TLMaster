using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class ItemService(IItemRepository itemRepository, IMapper mapper)
    : BaseService<ItemDto, Item>(itemRepository, mapper), IItemService
{
}
