import torch
from transformers import BertForSequenceClassification, AutoTokenizer
import fileinput
import numpy as np

def Pipe():
    tokenizer = AutoTokenizer.from_pretrained("vinai/bertweet-base", use_fast=False)
    model = BertForSequenceClassification.from_pretrained("vinai/bertweet-base", num_labels=3)

    sentences = []
    outputslist = []
    for line in fileinput.input():
        sentences.append(line)

    for sentence in sentences:
        inputs = tokenizer(sentence, return_tensors="pt")
        labels = torch.tensor([1]).unsqueeze(0)
        outputs = model(**inputs, labels=labels, output_attentions=True)
        outputslist.append(torch.nn.Softmax(outputs.logits.detach().numpy()).dim)
        np.save("/home/martin/RiderProjects/Lexonic/NewsTwitterDPM/SentimentNPYs/CurrentTweetSentiment.npy", np.stack(outputslist, axis = 0))
    return np.stack(outputslist, axis = 0)


if __name__ == '__main__':
    print(Pipe())
