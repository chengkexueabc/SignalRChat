<script setup>
import {ref,reactive,onMounted} from 'vue';
import axios from 'axios';
import myHub from './myHub'; //导入hub
const loginUser=reactive({
  userName:'王鹤棣',
  userPwd:'123456',
});
const loginBtn= ()=>{
  axios.post('http://localhost:5179/api/test/login',loginUser)
  .then(async res => {
    console.log(res);
    alert('成功');
    //这里在登录成功之后调用服务端在MyHub类的CreateConnection方法，
    //把登录成功之后返回的userId传过去
    //使客户端与服务端建立连接
    if(myHub.state.toString()!="Connected"){
        await myHub.start();
    }
    myHub.invoke("CreateConnection",res.data);
  })
}

//这里的ReceiveMessage用于接受服务器发送的消息
//这个ReceiveMessage名字是自己定义的
onMounted(() => {
  myHub.on('ReceiveMessage', (message) => {
    console.log("MyHub接受到的消息："+message);
    alert(message);
  })
})
const message=ref();
const sendUserId=ref();
const sendMessage=()=>{
  myHub.invoke("SendMessageToUser",Number(sendUserId.value),message.value)
}
</script>

<template>
  <input type="text" v-model.trim="loginUser.userName" placeholder="用户名">
  <input type="text" v-model.trim="loginUser.userPwd" placeholder="密码">
  <button @click="loginBtn">确定</button>
  <input type="text" v-model="message">
  <select  v-model="sendUserId">
    <option value="1">王鹤棣</option>
    <option value="2">吴磊</option>
    <option value="3">赵露思</option>
  </select >
  <button @click="sendMessage">发送消息</button>
</template>
