﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.Record;
using PersonalDiary.WebAPI.Extensions;
using System;
using System.Diagnostics;

namespace PersonalDiary.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;

        }

        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody] RecordCreateDTO recordCreateDTO)
        {
            var authorId = this.GetUserIdFromToken();
            var record = await _recordService.CreateRecord(recordCreateDTO, authorId);

            return Created(HttpContext.Request.Path, record);
        }

        [HttpGet("pageNumber")]
        public async Task<IActionResult> GetFiveRecords(int pageNumber)
        {
            var authorId = this.GetUserIdFromToken();
            var records = await _recordService.GetFiveRecords(authorId, pageNumber);

            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordById(Guid id)
        {
            var record = await _recordService.GetRecordById(id);

            return Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            await _recordService.DeleteRecord(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecord([FromBody] RecordUpdateDTO recordUpdateDTO)
        {
            var authorId = this.GetUserIdFromToken();
            await _recordService.UpdateRecord(recordUpdateDTO, authorId);

            return Ok();
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var authorId = this.GetUserIdFromToken();
            var records = await _recordService.GetRecordsByDate(date, authorId);

            return Ok(records);
        }
    }
}
