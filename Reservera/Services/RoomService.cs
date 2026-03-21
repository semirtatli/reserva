using AutoMapper;
using Reservera.DTOs;
using Reservera.Exceptions;
using Reservera.Models;
using Reservera.Repositories;

namespace Reservera.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;
    private readonly IMapper _mapper;

    public RoomService(IRoomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<RoomResponse>> GetAll()
    {
        var rooms = await _repository.GetAll();
        return _mapper.Map<List<RoomResponse>>(rooms);
    }

    public async Task<RoomResponse> GetById(int id)
    {
        var room = await _repository.GetById(id);
        if (room is null) throw new NotFoundException($"Id={id} olan oda bulunamadı.");
        return _mapper.Map<RoomResponse>(room);
    }

    public async Task<RoomResponse> Create(CreateRoomRequest request)
    {
        var room = _mapper.Map<Room>(request);
        var created = await _repository.Add(room);
        return _mapper.Map<RoomResponse>(created);
    }

    public async Task Delete(int id)
    {
        var deleted = await _repository.Delete(id);
        if (!deleted) throw new NotFoundException($"Id={id} olan oda bulunamadı.");
    }
}
