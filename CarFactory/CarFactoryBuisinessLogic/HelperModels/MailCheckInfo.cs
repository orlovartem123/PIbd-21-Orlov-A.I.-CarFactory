﻿using CarFactoryBusinessLogic.Interfaces;

namespace CarFactoryBusinessLogic.HelperModels
{
    public class MailCheckInfo
    {
        public string PopHost { get; set; }

        public int PopPort { get; set; }

        public IMessageInfoStorage Storage { get; set; }

        public IClientStorage ClientStorage { get; set; }
    }
}
