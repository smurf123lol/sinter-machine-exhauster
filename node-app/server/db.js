const sqlite3 = require('sqlite3').verbose();
const path = require('path');
const db = new sqlite3.Database(path.resolve(__dirname,'../../Backend/maindb.db'));

module.exports = db;