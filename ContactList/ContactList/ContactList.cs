using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList
{
    [ApiController]
    [Route("contactlist")]
    public partial class ToDoController : ControllerBase
    {
        private static readonly List<contact> contactList =
            new List<contact> { new contact(1, "herbert", "müller", "mueller@mail.at"), new contact(2, "Franz", "Bauer", "f.bauer@mail.at")};

        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(contactList);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] contact newItem)
        {
            contactList.Add(newItem);
            return CreatedAtRoute("GetSpecificItem", new { index = contactList.IndexOf(newItem) }, newItem);
        }

        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeleteItem(contact item)
        {
            if (item.id >= 0 && item.id < contactList.Count)
            {
                contactList.RemoveAt(item.id);
                return NoContent();
            }

            return BadRequest("Invalid Contact Id");
        }

        [HttpGet]
        [Route("{index}", Name = "GetSpecificItem")]
        public IActionResult GetItem(string lastName)
        {
            foreach(contact i in contactList)
            {
                if (i.lastName.Equals(lastName))
                {
                    return Ok(i);
                }
            }

            return BadRequest("Invalid lastName");
        }
    }
}
