﻿using System.Collections.Generic;
using Business.Abstract;

using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.BusinessAspects.Autofac;
using Core.Constants;
using Core.Utilities.Results;
using Core.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager:ICarService
    {
        private ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.CarsListed);

        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id)) ;
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id));
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car entity)
        {
            
            _carDal.Add(entity);
            return new SuccessResult(Messages.CarAdded);

        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails()) ;
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id)) ;
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult(Messages.CarUpdated);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car entity)
        {
            _carDal.Delete(entity);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByColorId(colorId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByBrandId(brandId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandAndColor(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByBrandAndColor(brandId, colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsById(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsById(carId));
        }
    }
}
