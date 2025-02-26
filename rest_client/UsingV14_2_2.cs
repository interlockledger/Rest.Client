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

using InterlockLedger.Rest.Client.Abstractions;
using InterlockLedger.Rest.Client.V14_2_2;

namespace rest_client;

public class UsingV14_2_2(RestAbstractNode<RestChainV14_2_2> node) : AbstractUsing<RestChainV14_2_2>(node)
{
    protected override string Version => "14.2.2";

    protected override async Task DoExtraExercisesAsync(RestAbstractNode<RestChainV14_2_2> node, RestChainV14_2_2 chain, bool write) {
        await ExerciseJsonDocumentsV14_2_2Async(chain, node.Certificate, write).ConfigureAwait(false);
        await ExerciseOpaqueRecordsAsync(chain, write).ConfigureAwait(false);
    }

    private static async Task ExerciseJsonDocumentsV14_2_2Async(RestChainV14_2_2 chain, X509Certificate2 certificate, bool write) {
        try {
            Console.WriteLine("  JsonDocuments:");
            if (write) {
                // Add something
            }
            await ReadSomeJsonDocumentsRecordsAsync(certificate, chain.JsonStore, chain.Records, chain.Id, 2100, "jsonDocuments", RetrieveAndDumpJsonDocumentsAsync).ConfigureAwait(false);
            var query = await chain.JsonStore.RetrieveAllowedReaders(chain.Id).ConfigureAwait(false);
            if (query.TotalNumberOfPages > 0) {
                Console.WriteLine($"    RetrieveAllowedReadersAsync retrieved first page of {query.TotalNumberOfPages} pages with {query.Items.Count()} items");
                Console.WriteLine(query.First()?.AsJson());
            } else {
                Console.WriteLine($"    RetrieveAllowedReadersAsync retrieved no data");
            }
        } catch (Exception e) {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine();
    }

    private static async Task ExerciseOpaqueRecordsAsync(RestChainV14_2_2 chain, bool write) {
        Console.WriteLine("  OpaqueRecords:");
        var opaqueStore = chain.OpaqueStore;
        var query = await opaqueStore.QueryRecordsFromAsync(appId: 13).ConfigureAwait(false);
        Console.WriteLine($"    LastChangedRecordSerial {query?.LastChangedRecordSerial} for {chain.Id}");
        try {
            ulong serialToRetrieve = 0;
            if (write) {
                Console.WriteLine("    Trying to add an opaque payload #13,100");
                var result = await opaqueStore.AddRecordAsync(appId: 13, payloadTypeId: 100, query.LastChangedRecordSerial, [1, 2, 3, 4]).ConfigureAwait(false);
                serialToRetrieve = result.Serial;
            } else {
                serialToRetrieve = query.First()?.Serial ?? 0;
            }
            var response = await opaqueStore.RetrieveSinglePayloadAsync(serialToRetrieve).ConfigureAwait(false);
            if (response.HasValue) {
                Console.WriteLine($"    Retrieved AppId: {response.Value.AppId}");
                Console.WriteLine($"    Retrieved PayloadTypeId: {response.Value.PayloadTypeId}");
                Console.WriteLine($"    Retrieved CreatedAt: {response.Value.CreatedAt}");
                Console.WriteLine($"    Retrieved Bytes: {response.Value.Content.BigEndianReadUInt():x}");
            } else {
                Console.WriteLine("    Could not retrieve opaque payload");
            }
        } catch (Exception ex) {
            if (ex is AggregateException ae) {
                Console.WriteLine(ae.Message);
            } else {
                Console.WriteLine(ex.Message);
            }
        }
        Console.WriteLine();
    }

}