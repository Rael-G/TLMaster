using System;

namespace TLMaster.Api.Interfaces;

public interface IInputModel<TDto>
{
    TDto InputToDto();

    void InputToDto(TDto dto);
}
