﻿using System;
using System.Collections.Generic;
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace FlashcardsClient
{
    public class Test
    {
        public Guid TestId { get; set; }
        public List<ExerciseQuestion> Exercises { get; set; }
    }
}