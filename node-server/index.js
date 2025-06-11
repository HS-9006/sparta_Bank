const express = require('express');
const mysql = require('mysql2');
const cors = require('cors');

const app = express();
const port = 3000;

// 미들웨어
app.use(cors());
app.use(express.json()); // bodyParser 대체

// DB 연결
const db = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'root1234',
    database: 'atmdb'
});

db.connect(err => {
    if (err) {
        console.error('MySQL 연결 실패:', err);
        return;
    }
    console.log('MySQL 연결 성공');
});

// 로그인 API
app.post('/api/login', (req, res) => {
    const { userName, password } = req.body;

    if (!userName || !password) {
        return res.status(400).json({ error: 'userName과 password는 필수입니다' });
    }

    const sql = 'SELECT * FROM users WHERE userName = ? AND password = ?';
    db.query(sql, [userName, password], (err, results) => {
        if (err) {
            console.error('로그인 쿼리 실패:', err);
            return res.status(500).json({ error: 'DB 오류' });
        }

        if (results.length === 0) {
            return res.status(401).json({ error: '잘못된 사용자 정보입니다' });
        }

        const user = results[0];
        res.status(200).json({
            userName: user.userName,
            name: user.name,
            cash: user.cash,
            bankBalance: user.bankBalance
        });
    });
});

// 회원가입 API - 중복 체크 포함
app.post('/api/register', (req, res) => {
    const { userName, password, name } = req.body;

    // 중복 체크
    db.query('SELECT * FROM users WHERE userName = ?', [userName], (err, results) => {
        if (err) return res.status(500).json({ error: 'DB 오류' });

        if (results.length > 0) {
            return res.status(409).json({ error: '이미 존재하는 사용자입니다.' });
        }

        // 새 유저 등록
        const sql = 'INSERT INTO users (userName, password, name, cash, bankBalance) VALUES (?, ?, ?, 0, 0)';
        db.query(sql, [userName, password, name], err => {
            if (err) return res.status(500).json({ error: '회원가입 실패' });
            res.send('회원가입 성공');
        });
    });
});

// 사용자 정보 조회 (GET)
app.get('/user/:userName', (req, res) => {
    const userName = req.params.userName;

    const sql = 'SELECT userName, name, cash, bankBalance FROM users WHERE userName = ?';
    db.query(sql, [userName], (err, result) => {
        if (err) {
            console.error('조회 실패:', err);
            return res.status(500).json({ error: 'DB 조회 중 오류 발생' });
        }

        if (result.length === 0) {
            return res.status(404).json({ error: '해당 사용자를 찾을 수 없습니다' });
        }

        res.status(200).json(result[0]);
    });
});

// 사용자 정보 저장 (POST)
app.post('/user/:userName', (req, res) => {
    const userName = req.params.userName;
    const { name, cash, bankBalance } = req.body;

    if (!name || cash == null || bankBalance == null) {
        return res.status(400).json({ error: '모든 필드를 채워주세요' });
    }

    const sql = 'UPDATE users SET name = ?, cash = ?, bankBalance = ? WHERE userName = ?';
    db.query(sql, [name, cash, bankBalance, userName], err => {
        if (err) {
            console.error('업데이트 실패:', err);
            return res.status(500).json({ error: 'DB 저장 중 오류 발생' });
        }

        res.status(200).send('저장 완료');
    });
});

// 서버 시작
app.listen(port, () => {
    console.log(`서버 실행 중: http://localhost:${port}`);
});