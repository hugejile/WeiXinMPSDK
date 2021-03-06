﻿/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ResponseMessageText.cs
    文件功能描述：响应回复文本消息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class ResponseMessageScanProduct : ResponseMessageBase, IResponseMessageBase
    {
        new public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.ScanProduct; }
        }

        public ResponseMessageScanProduct_ScanProduct ScanProduct { get; set; }

    }

    public class ResponseMessageScanProduct_ScanProduct
    {
        public string KeyStandard { get; set; }

        public string KeyStr { get; set; }

        public string ExtInfo { get; set; }

        public ResponseMessageScanProduct_AntiFake AntiFake { get; set; }
    }

    public class ResponseMessageScanProduct_AntiFake
    {
        public string CodeResult { get; set; }
    }
}
