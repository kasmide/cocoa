﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using Covid19Radar.Api.Models;
using System;
using System.Threading.Tasks;

namespace Covid19Radar.Api.DataAccess
{
    public interface IDiagnosisRepository
    {

        Task<DiagnosisModel[]> GetNotApprovedAsync();
        Task<DiagnosisModel> GetAsync(string submissionNumber, string userUuid);

        Task<DiagnosisModel> SubmitDiagnosisAsync(string submissionNumber, DateTimeOffset timestamp, string userUuid, TemporaryExposureKeyModel[] Keys);

        Task DeleteAsync(IUser user);
    }
}
