using AutoMapper;
using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Domain.Entities;
using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CeciAdminMT.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ResultDataResponse<IEnumerable<CompanyResultDTO>>> GetAsync(CompanyFilterDTO filter)
        {
            var response = new ResultDataResponse<IEnumerable<CompanyResultDTO>>();

            try
            {
                response.Data = _mapper.Map<IEnumerable<CompanyResultDTO>>(await _uow.Company.GetByFilterAsync(filter));
                response.TotalItems = await _uow.Company.GetTotalByFilterAsync(filter);
                response.TotalPages = (int)Math.Ceiling((double)response.TotalItems / filter.PerPage);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public async Task<ResultResponse> AddAsync(CompanyAddDTO obj)
        {
            var response = new ResultResponse();

            try
            {
                var companyEntity = _mapper.Map<Company>(obj);
                companyEntity.ApiKey = Guid.NewGuid().ToString();
                await _uow.Company.AddAsync(companyEntity);
                await _uow.CommitAsync();

                response.Message = "Company successfully added.";
            }
            catch (Exception ex)
            {
                response.Message = "Could not add company.";
                response.Exception = ex;
            }

            return response;
        }

        public async Task<ResultResponse> DeleteAsync(CompanyDeleteDTO obj)
        {
            var response = new ResultResponse();

            try
            {
                var company = await _uow.Company.GetFirstOrDefaultAsync(c => c.Id == obj.CompanyId);

                _uow.Company.Delete(company);
                await _uow.CommitAsync();

                response.Message = "Company successfully deleted.";
            }
            catch (Exception ex)
            {
                response.Message = "Could not deleted company.";
                response.Exception = ex;
            }

            return response;
        }

        public async Task<ResultResponse> UpdateAsync(CompanyUpdateDTO obj)
        {
            var response = new ResultResponse();

            try
            {
                var company = await _uow.Company.GetFirstOrDefaultAsync(c => c.Id == obj.CompanyId);

                company = _mapper.Map(obj, company);

                _uow.Company.Update(company);
                await _uow.CommitAsync();

                response.Message = "Company successfully updated.";
            }
            catch (Exception ex)
            {
                response.Message = "Could not updated company.";
                response.Exception = ex;
            }

            return response;
        }

        public async Task<ResultResponse<CompanyResultDTO>> GetByIdAsync(int id)
        {
            var response = new ResultResponse<CompanyResultDTO>();

            try
            {
                response.Data = _mapper.Map<CompanyResultDTO>(await _uow.Company.GetFirstOrDefaultNoTrackingAsync(x=> x.Id.Equals(id)));
            }
            catch (Exception ex)
            {
                response.Message = "It was not possible to search the company.";
                response.Exception = ex;
            }

            return response;
        }
    }
}
