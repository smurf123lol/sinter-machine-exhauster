
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
  app.get("/getexdata", (req, res) => {
    let exID = req.query.id;
    const sql1 = `SELECT 
    *
    FROM exgausters
    WHERE id=`+exID

    let result ={}
    
    db.all(sql1,(err,rows)=>{
      result.exgauster = rows[0];
      if(result.bearings)res.json(result);
    });
    const sql2 = `SELECT 
    *
    FROM bearings
    WHERE exgauster_id=`+exID
    db.all(sql2,(err,rows)=>{
      result.bearings = rows;
      if(result.exgauster)res.json(result);
    });
  })

  app.listen(PORT, () => {
    console.log(`Server listening on ${PORT}`);
  });
}

db.serialize(()=>{
  StartExApp();
});