using AutoMapper;
using DiaCare.Application.DTOS;
using DiaCare.Application.Interfaces;
using DiaCare.Domain.Entities;
using DiaCare.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaCare.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IBaseRepository<Article> _baseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleService(IBaseRepository<Article> baseRepository, IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _unitOfWork = unitOfWork;

            _mapper = mapper;

        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesAsync()
        {
            var articles = await _baseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ArticleDto>>(articles);
        }

        public async Task<ArticleDto> GetByIdAsync(int id)
        {
            var exist = await _baseRepository.GetByIdAsync(id);
            return _mapper.Map<ArticleDto>(exist);
        }

        public async Task<ArticleDto> AddArticleAsync(ArticleDto dto)
        {
            var article = _mapper.Map<Article>(dto);
            await _baseRepository.AddAsync(article);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ArticleDto>(article);
        }
        public async Task<ArticleDto> UpdateArticleAsync(int id, ArticleDto dto)
        {
            var existingArticle = await _baseRepository.GetByIdAsync(id);
            if (existingArticle == null) return null;

           
            _mapper.Map(dto, existingArticle);

            _baseRepository.Update(existingArticle);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ArticleDto>(existingArticle);
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _baseRepository.GetByIdAsync(id);
            if (article == null) return false;

            _baseRepository.Delete(article);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


    }

}