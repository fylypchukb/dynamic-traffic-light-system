import React from "react";
import {Layers} from "lucide-react";
import {Card, CardContent, CardHeader, CardTitle} from "@/components/ui/card";
import {Separator} from "@/components/ui/separator";
import {TrafficLightForm} from "./traffic-light-form";

export default function TrafficLightPage({params}: { params: { id: string } }) {
    const isNew = params.id === "new";
    const title = isNew ? "Create Traffic Light" : `Edit Traffic Light ${params.id}`;

    return (
        <div className="container mx-auto py-8">
            <Card className="max-w-4xl mx-auto">
                <CardHeader>
                    <div className="flex items-center gap-2">
                        <Layers className="h-6 w-6 text-primary"/>
                        <CardTitle>{title}</CardTitle>
                    </div>
                </CardHeader>
                <Separator/>
                <CardContent className="pt-6">
                    <TrafficLightForm id={isNew ? null : parseInt(params.id)}/>
                </CardContent>
            </Card>
        </div>
    );
}