
const express = require("express");

const PORT = process.env.PORT || 3001;

const db = require('./db');

const app = express();

function StartExApp(){
  app.get("/getmachines", (req, res) => {
    
      const sql = `SELECT 
      aglomachines.id,
      aglomachines.name,
      exgausters.id as ex_id,
      exgausters.name as ex_name
      FROM aglomachines
      RIGHT JOIN exgausters ON aglomachines.id = exgausters.aglomachine_id`;

      db.all(sql,(err,rows)=>{
        res.json(rows);
    });
  })

  app.listen(PORT, () => {
    console.log(`Server listening on ${PORT}`);
  });
}

db.serialize(()=>{
  StartExApp();
});