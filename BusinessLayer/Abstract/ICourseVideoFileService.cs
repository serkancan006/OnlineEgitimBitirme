﻿using EntityLayer.Concrete.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICourseVideoFileService : IGenericService<CourseVideoFile>
    {
        public List<CourseVideoFile> CourseGetVideoFiles(int id);
    }
}
