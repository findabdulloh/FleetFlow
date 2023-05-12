﻿using FleetFlow.Domain.Commons;

namespace FleetFlow.Domain.Entities;

public class Attachment : Auditable
{
    public string FilePath { get; set; }
    public string FileName { get; set; }
}
