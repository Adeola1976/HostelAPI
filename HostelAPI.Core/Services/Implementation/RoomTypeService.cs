using AutoMapper;
using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Model;
using HostelAPI.Utilities.DTOs;
using HostelAPI.Utilities.DTOs.Rooms;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Services.Implementation
{
    public class RoomTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RoomTypeService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Response<RoomTypeResponseDto>> AddRoomType(RoomTypeRequestDto roomTypeDto)
        {
            var GetRoomTypeByName = _unitOfWork.RoomType.GetRoomTypeByName(roomTypeDto.Name);
            if (GetRoomTypeByName==null)
            {
                var roomType = _mapper.Map<RoomType>(roomTypeDto);
                await _unitOfWork.RoomType.InsertAsync(roomType);
                await _unitOfWork.Save();
                var response = _mapper.Map<RoomTypeResponseDto>(roomType);
                return Response<RoomTypeResponseDto>.Success($"{response.Name} entered successfully", response);
            }
            return Response<RoomTypeResponseDto>.Fail("RoomType already exist");
        }

        public async Task<Response<RoomTypeResponseDto>>  UpdateRoomType(string RoomTypeId, RoomTypeRequestDto roomTypeDto)
        {
           
            var GetRoomTypeEntity = _unitOfWork.RoomType.FindEntityById(RoomTypeId);
            if (GetRoomTypeEntity != null)
            {
                GetRoomTypeEntity.Name = string.IsNullOrWhiteSpace(GetRoomTypeEntity.Name) ? GetRoomTypeEntity.Name : roomTypeDto.Name;
                GetRoomTypeEntity.Price = string.IsNullOrWhiteSpace(GetRoomTypeEntity.Price.ToString()) ? GetRoomTypeEntity.Price : roomTypeDto.Price;
                GetRoomTypeEntity.Description = string.IsNullOrWhiteSpace(GetRoomTypeEntity.Description) ? GetRoomTypeEntity.Description : roomTypeDto.Description;

                _unitOfWork.RoomType.Update(GetRoomTypeEntity);
                await _unitOfWork.Save();
                var response = _mapper.Map<RoomTypeResponseDto>(GetRoomTypeEntity);
                return Response<RoomTypeResponseDto>.Success($"{ roomTypeDto.Name} updated sucessfully", response);
            }
            return Response<RoomTypeResponseDto>.Fail("oops roomtype doesn't exist ");
        }

           public  Response<RoomTypeResponseDto> GetRoomType()
        {
                IEnumerable<RoomType> roomType =   _unitOfWork.RoomType.GetEntity();
                var response = _mapper.Map<RoomTypeResponseDto>(roomType);
                return Response<RoomTypeResponseDto>.Success($"data recieved successfully",response);
        }


        public bool DeleteRoomType(string RoomTypeId)
        {
            var GetRoomTypeEntity = _unitOfWork.RoomType.FindEntityById(RoomTypeId);
            if (GetRoomTypeEntity != null)
            {
                _unitOfWork.RoomType.DeleteAsync(GetRoomTypeEntity);
                return true;
            }
            return false;

        }


    } 
}
