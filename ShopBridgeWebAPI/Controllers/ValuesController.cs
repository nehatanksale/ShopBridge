using ShopBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridgeWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public IHttpActionResult Get(bool includeAddress = false)
        {
            IList<Item> items = null;

            var ctx = new InventoryEntities();
            
                items = ctx.Items.Select(x => x).ToList();


            if (items.Count == 0)
            {
                return NotFound();
            }

            return Ok(items);
        }


        // POST api/values
        //public void Post([FromBody] string value)
        //{

        //}
        public IHttpActionResult Post(Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new InventoryEntities())
            {
                ctx.Items.Add(new Item()
                {
                    item_name = item.item_name,
                    item_desc = item.item_desc,
                    item_price = item.item_price,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        // PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}
        IHttpActionResult Put(Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new InventoryEntities())
            {
                var existingStudent = ctx.Items.Where(s => s.item_id == item.item_id)
                                                        .FirstOrDefault<Item>();

                if (existingStudent != null)
                {
                    existingStudent.item_name = item.item_name;
                    existingStudent.item_desc = item.item_desc;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new InventoryEntities())
            {
                var student = ctx.Items
                    .Where(s => s.item_id == id)
                    .FirstOrDefault();

                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
