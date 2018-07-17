using System;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Automation.Mapper;

namespace AMKsGear.Core.Automation.Mapper
{
    public class MapperBuilder : IMapperBuilder
    {
        private bool _seaded;

        public bool CustomMappingEnabled { get; set; }
        public bool CustomMappingCacheEnabled { get; set; }


        public MapperBuilder()
        {
            CustomMappingEnabled = true;
            CustomMappingCacheEnabled = true;
        }

        #region Fluent Interface
        
        public MapperBuilder AllowCustomMapping(bool state)
        {
            ThrowIfSealed();
            CustomMappingEnabled = state;
            return this;
        }
        public MapperBuilder AllowCustomMappingCache(bool state)
        {
            ThrowIfSealed();
            CustomMappingCacheEnabled = state;
            return this;
        }
        
        
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfSealed()
        {
            if (_seaded)
            {
                throw new InvalidOperationException();
            }
        }

        public void Seal()
        {
            _seaded = true;
        }

        public bool CheckAndSeal()
        {

            return false;
        }

        public IMapper Compile()
        {

            return null;
        }
    }
}