// ******************************************************************************************************************************
//
// Copyright (c) 2018-2022 InterlockLedger Network
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met
//
// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
//
// * Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived from
//   this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES, LOSS OF USE, DATA, OR PROFITS, OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// ******************************************************************************************************************************

namespace InterlockLedger.Rest.Client;

/// <summary>
/// Base class for RecordModel
/// </summary>
public abstract class RecordModelBase
{
    /// <summary>
    /// Application id this record is associated with
    /// </summary>
    public ulong ApplicationId { get; set; }

    /// <summary>
    /// chain id that owns this record
    /// </summary>
    public required string ChainId { get; set; }

    /// <summary>
    /// Time of record creation
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// IL2 Network
    /// </summary>
    public required string Network { get; set; }

    /// <summary>
    /// The payload's TagId
    /// </summary>
    public ulong PayloadTagId { get; set; }

    /// <summary>
    /// Record universal reference [Network]:[ChainId]@[Serial]
    /// </summary>
    public required UniversalRecordReference Reference { get; set; }

    /// <summary>
    /// Record serial number.
    /// For the first record this value is zero (0)
    /// </summary>
    public ulong Serial { get; set; }


    /// <summary>
    /// Block type
    /// Most records are of the type 'Data'
    /// </summary>
    public RecordType Type { get; set; }

    /// <summary>
    /// Version of this record structure
    /// </summary>
    public ushort Version { get; set; }

}